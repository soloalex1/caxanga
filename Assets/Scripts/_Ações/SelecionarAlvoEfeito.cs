using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Selecionar Alvo do Efeito")]
public class SelecionarAlvoEfeito : Acao
{
    public EstadoJogador faseDeControle;
    public VariavelTransform gridAreaDropavel;
    public EstadoJogador jogadorUsandoEfeito;
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                SeguradorDeJogador jogadorInimigo = Configuracoes.admJogo.jogadorInimigo;

                //logica para atacar o jogador inimigo
                InfoUIJogador infoJogadorAlvo = r.gameObject.GetComponentInParent<InfoUIJogador>();
                if (infoJogadorAlvo != null)
                {
                    if (infoJogadorAlvo.jogador == jogadorInimigo)
                    {
                        Configuracoes.admJogo.efeitoAtual.jogadorAlvo = jogadorInimigo;
                    }
                    else
                    {
                        Configuracoes.admJogo.efeitoAtual.jogadorAlvo = Configuracoes.admJogo.jogadorLocal;

                    }

                    Configuracoes.admJogo.DefinirEstado(faseDeControle);
                    Configuracoes.RegistrarEvento("O alvo " + Configuracoes.admJogo.efeitoAtual.jogadorAlvo.nomeJogador + " foi selecionado para sofrer o efeito", Color.white);
                    Configuracoes.admJogo.efeitoAtual.ExecutarEfeito();

                    if (gridAreaDropavel != null)
                    {
                        gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                    }
                }

                //logica para atacar uma carta
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (instCarta != null)
                {
                    //clicou na mesma carta para cancelar o efeito
                    if (instCarta == Configuracoes.admJogo.efeitoAtual.cartaQueInvoca && !Configuracoes.admJogo.efeitoAtual.podeUsarEmSi)
                    {
                        Configuracoes.admJogo.DefinirEstado(faseDeControle);
                        Configuracoes.RegistrarEvento("O jogador desistiu de usar o efeito", Color.white);
                        Configuracoes.admJogo.efeitoAtual = null;
                        return;
                    }
                    //selecionar uma carta como alvo do efeito
                    if (instCarta != null && instCarta.podeSofrerEfeito)
                    {
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        Configuracoes.admJogo.efeitoAtual.cartaAlvo = instCarta;
                        Configuracoes.admJogo.DefinirEstado(faseDeControle);
                        Configuracoes.RegistrarEvento("O alvo " + instCarta.infoCarta.carta.name + " foi selecionado para sofrer o efeito", Color.white);
                        Configuracoes.admJogo.efeitoAtual.ExecutarEfeito();
                        return;
                    }
                }

            }
            //clicar em qualquer lugar da tela
            if (Configuracoes.admJogo.estadoAtual == jogadorUsandoEfeito)
            {
                if (gridAreaDropavel != null)
                {
                    gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                }
                Configuracoes.admJogo.DefinirEstado(faseDeControle);
                Configuracoes.RegistrarEvento("O jogador desistiu de usar o efeito", Color.white);
                Configuracoes.admJogo.efeitoAtual = null;
                return;
            }
        }
    }
}
