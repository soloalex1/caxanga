using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Lógicas/CartaBaixada")]
public class CartaBaixada : LogicaInstanciaCarta
{
    public GameEvent aoOlharCarta;
    public VariavelCarta cartaAtual;
    public EstadoJogador usandoEfeito;
    public Sprite cursorAlvoVerde, cursorAlvoVermelho;

    public override void AoClicar(InstanciaCarta carta)
    {
        if (carta.efeito != null
            && carta.efeito.ativacao.nomeAtivacao == "Ativa"
            && carta.efeitoUsado == false
            && Configuracoes.admJogo.estadoAtual.name == "Em Fase de Controle")
        {
            Configuracoes.RegistrarEvento(carta.infoCarta.carta.name + " foi selecionado(a) para ativar o efeito", Color.white);
            Configuracoes.admJogo.efeitoAtual = carta.efeito;
            Configuracoes.admJogo.DefinirEstado(usandoEfeito);
            carta.gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        }
        else
        {
            if (carta.efeitoUsado == true)
            {
                Configuracoes.RegistrarEvento(carta.infoCarta.carta.name + " já usou seu efeito", Color.white);
            }
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
                    Configuracoes.admCursor.MudarSprite(cursorAlvoVerde);
                }
                else
                {
                    Configuracoes.admCursor.MudarSprite(cursorAlvoVermelho);
                }
            }
        }
    }
}