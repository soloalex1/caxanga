using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Selecionar Alvo Atacado")]
public class SelecionarAlvoAtacado : Acao
{
    public GameEvent cartaFoiAtacada;
    public EstadoJogador jogadorEmSeuTurno;
    public VariavelTransform gridAreaDropavel;
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
                        Configuracoes.admJogo.DefinirEstado(jogadorEmSeuTurno);
                        Configuracoes.RegistrarEvento("O alvo " + Configuracoes.admJogo.jogadorAtacado.nomeJogador + " foi selecionado para ser atacado", Color.white);
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.Atacar());
                        Configuracoes.admJogo.jogadorAtual.fezAlgumaAcao = true;
                    }
                    else
                    {
                        Configuracoes.admJogo.TocarSomNaoPode();

                    }
                    break;
                }
                //logica para atacar uma carta
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (instCarta != null)
                {
                    if (instCarta == Configuracoes.admJogo.cartaAtacante)
                    {
                        Configuracoes.admJogo.cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                        Configuracoes.RegistrarEvento("O jogador desistiu de atacar", Color.white);
                        Configuracoes.admJogo.estadoAtual = jogadorEmSeuTurno;
                        return;
                    }

                    if (instCarta.podeSerAtacada)
                    {
                        Configuracoes.admJogo.cartaAtacada = instCarta;
                        Configuracoes.admJogo.DefinirEstado(jogadorEmSeuTurno);
                        cartaFoiAtacada.cartaQueAtivouEvento = instCarta;
                        Configuracoes.admEfeito.eventoAtivador = cartaFoiAtacada;
                        cartaFoiAtacada.Raise();
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.Atacar());
                        Configuracoes.admJogo.jogadorAtual.fezAlgumaAcao = true;
                        if (gridAreaDropavel != null)
                        {
                            gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
                        }
                        return;
                    }
                    else
                    {
                        Configuracoes.admJogo.TocarSomNaoPode();
                    }
                }
            }

            if (gridAreaDropavel != null)
            {
                gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = true;
            }
            Configuracoes.admJogo.cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
            Configuracoes.admJogo.estadoAtual = jogadorEmSeuTurno;

        }
    }
}
