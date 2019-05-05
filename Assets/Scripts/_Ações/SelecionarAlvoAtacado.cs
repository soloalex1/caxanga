using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Selecionar Alvo Atacado")]
public class SelecionarAlvoAtacado : Acao
{
    public EstadoJogador faseDeBatalha;
    public VariavelTransform gridAreaDropavel;
    public EstadoJogador jogadorAtacando;
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                SeguradorDeJogador jogadorInimigo = null;
                if (Configuracoes.admJogo.jogadorAtual == Configuracoes.admJogo.jogadorLocal)
                {
                    jogadorInimigo = Configuracoes.admJogo.jogadorInimigo;
                }
                else
                {
                    jogadorInimigo = Configuracoes.admJogo.jogadorLocal;
                    Debug.Log("O jogador inimigo é o " + jogadorInimigo.nomeJogador);
                }
                //logica para atacar o jogador inimigo
                InfoUIJogador infoJogadorInimigo = r.gameObject.GetComponent<InfoUIJogador>();
                if (infoJogadorInimigo != null)
                {
                    if (infoJogadorInimigo.jogador == jogadorInimigo)
                    {
                        Configuracoes.admJogo.jogadorAtacado = jogadorInimigo;
                        Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                        Configuracoes.RegistrarEvento("O alvo " + jogadorInimigo.nomeJogador + " foi selecionado para ser atacado", Color.white);
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                    } else {
                        Configuracoes.admJogo.jogadorAtacado = Configuracoes.admJogo.jogadorLocal;
                        Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                        Configuracoes.RegistrarEvento("O alvo " + Configuracoes.admJogo.jogadorLocal.nomeJogador + " foi selecionado para ser atacado", Color.white);
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                    }
                }
                //logica para atacar uma carta
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (instCarta == Configuracoes.admJogo.cartaAtacante)
                {
                    Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                    return;
                }
                if (!jogadorInimigo.cartasBaixadas.Contains(instCarta))
                    return;
                if (instCarta.podeSerAtacada)
                {
                    Configuracoes.admJogo.cartaAtacada = instCarta;
                    Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                    Configuracoes.RegistrarEvento("O alvo " + instCarta.infoCarta.carta.name + " foi selecionado para ser atacado", Color.white);
                    if (gridAreaDropavel != null)
                    {
                        gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                    }
                    return;
                }
            }

            if (Configuracoes.admJogo.estadoAtual == jogadorAtacando)
            {
                if (gridAreaDropavel != null)
                {
                    gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                }
                Configuracoes.RegistrarEvento("O jogador desistiu de atacar", Color.white);
                Configuracoes.admJogo.estadoAtual = faseDeBatalha;
            }
        }
    }
}
