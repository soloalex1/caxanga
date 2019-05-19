using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Cartas/Lógicas/CartaBaixada")]
public class CartaBaixada : LogicaInstanciaCarta
{
    public GameEvent aoOlharCarta, cartaAtacou;
    public VariavelCarta cartaAtual;
    public EstadoJogador usandoEfeito, atacando;
    public Sprite cursorAlvoVerde, cursorAlvoVermelho, cursorClicavel;
    public VariavelTransform gridAreaDropavel;



    public override void AoClicar(InstanciaCarta carta)
    {
        if (carta.podeAtacarNesteTurno)
        {
            //Carta vai atacar
            if (gridAreaDropavel != null)
            {
                gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = false;
            }
            cartaAtacou.cartaQueAtivouEvento = carta;
            Configuracoes.admEfeito.eventoAtivador = cartaAtacou;
            cartaAtacou.Raise();
            Configuracoes.admJogo.DefinirEstado(atacando);
            Configuracoes.admJogo.cartaAtacante = carta;
            Configuracoes.admJogo.cartaAtacante.gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 1);
        }
    }
    public override void AoOlhar(InstanciaCarta carta)
    {
        if (carta != cartaAtual.valor)//se for diferente
        {
            cartaAtual.Set(carta);
            aoOlharCarta.Raise();
            if (Configuracoes.admJogo.estadoAtual == usandoEfeito)
            {
                if (carta.podeSofrerEfeito)
                {
                    if (Configuracoes.admJogo.jogadorAtual.cartasBaixadas.Contains(carta))
                    {
                        if (carta.efeito.podeUsarEmSi)
                        {
                            Configuracoes.admCursor.MudarSprite(cursorAlvoVerde);
                        }
                        else
                        {
                            Configuracoes.admCursor.MudarSprite(cursorAlvoVermelho);
                        }
                    }
                    if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(carta))
                    {
                        Configuracoes.admCursor.MudarSprite(cursorAlvoVerde);
                    }

                    return;
                }
                else
                {
                    Configuracoes.admCursor.MudarSprite(cursorAlvoVermelho);
                    return;
                }
            }
            if (Configuracoes.admJogo.estadoAtual == atacando)
            {
                if (carta.podeSerAtacada && carta != Configuracoes.admJogo.cartaAtacante)
                {
                    Configuracoes.admCursor.MudarSprite(cursorAlvoVerde);
                    return;
                }
                else
                {
                    Configuracoes.admCursor.MudarSprite(cursorAlvoVermelho);
                    return;
                }
            }
        }
        if (carta.podeAtacarNesteTurno && Configuracoes.admJogo.estadoAtual.name == "Em Seu Turno")
            Configuracoes.admCursor.MudarSprite(cursorClicavel);
    }
}