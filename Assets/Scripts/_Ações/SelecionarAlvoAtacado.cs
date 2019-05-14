using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Selecionar Alvo Atacado")]
public class SelecionarAlvoAtacado : Acao
{
    public GameEvent cartaFoiAtacada;
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
                //logica para atacar o jogador inimigo
                InfoUIJogador infoJogadorInimigo = r.gameObject.GetComponentInParent<InfoUIJogador>();
                if (infoJogadorInimigo != null)
                {
                    if (infoJogadorInimigo.jogador == Configuracoes.admJogo.jogadorInimigo)
                    {
                        Configuracoes.admJogo.jogadorAtacado = Configuracoes.admJogo.jogadorInimigo;
                        Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                        Configuracoes.RegistrarEvento("O alvo " + Configuracoes.admJogo.jogadorAtacado.nomeJogador + " foi selecionado para ser atacado", Color.white);
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.Atacar());

                    }
                    else
                    {
                        Configuracoes.admJogo.jogadorAtacado = Configuracoes.admJogo.jogadorLocal;
                        Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                        Configuracoes.RegistrarEvento("O alvo " + Configuracoes.admJogo.jogadorLocal.nomeJogador + " foi selecionado para ser atacado", Color.white);

                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.Atacar());

                    }
                    return;
                }
                //logica para atacar uma carta
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (instCarta != null)
                {
                    if (instCarta == Configuracoes.admJogo.cartaAtacante)
                    {
                        Configuracoes.admJogo.cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                        Configuracoes.RegistrarEvento("O jogador desistiu de atacar", Color.white);
                        Configuracoes.admJogo.estadoAtual = faseDeBatalha;
                        return;
                    }

                    if (instCarta.podeSerAtacada)
                    {
                        Configuracoes.admJogo.cartaAtacada = instCarta;
                        Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
                        if (instCarta.efeito != null && instCarta.efeito.eventoAtivador == cartaFoiAtacada)
                        {
                            Configuracoes.admJogo.StartCoroutine("ExecutarEfeito", instCarta.efeito);
                        }
                        Configuracoes.RegistrarEvento("O alvo " + instCarta.infoCarta.carta.name + " foi selecionado para ser atacado", Color.white);
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.Atacar());
                        return;
                    }
                }
            }

            if (Configuracoes.admJogo.estadoAtual == jogadorAtacando)
            {
                if (gridAreaDropavel != null)
                {
                    gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                }
                Configuracoes.admJogo.cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                Configuracoes.RegistrarEvento("O jogador desistiu de atacar", Color.white);
                Configuracoes.admJogo.estadoAtual = faseDeBatalha;
            }
        }
    }
}
