using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Ações/Ativar Efeito Carta")]
public class AtivarEfeitoCarta : Acao
{
    public EstadoJogador faseDeControle;
    public override void Executar(float d)
    {

        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                //se a carta estiver no campo do jogador atual
                SeguradorDeJogador jogadorAtual = Configuracoes.admJogo.jogadorAtual;

                if (jogadorAtual.cartasBaixadas.Contains(instCarta))
                {

                    if (jogadorAtual.podeUsarEfeito)
                    {
                        if (!instCarta.efeitoUsado)
                        {
                            Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
                            if (instCarta.efeito.podeUsarEmSi == true)
                            {
                                Configuracoes.RegistrarEvento("A carta " + instCarta.infoCarta.carta.name + " vai usar o seu efeito", Color.white);
                                Configuracoes.RegistrarEvento("Escolha um alvo para o efeito de " + instCarta.infoCarta.carta.name, Color.white);
                                Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
                                return;
                            }
                            else
                            {
                                //se existir cartas no campo do inimigo
                                if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Count > 0)
                                {
                                    Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
                                    if (Configuracoes.admJogo.efeitoAtual.modoEfeito == "")
                                    {
                                        Configuracoes.RegistrarEvento("Esta carta não tem efeito", Color.white);
                                        Configuracoes.admJogo.DefinirEstado(faseDeControle);
                                        Configuracoes.admJogo.efeitoAtual = null;
                                        return;
                                    }
                                    //se a carta pode usar o efeito em si mesma
                                    Configuracoes.RegistrarEvento("A carta " + instCarta.infoCarta.carta.name + " vai usar o seu efeito", Color.white);
                                    return;
                                }
                                //se não existir cartas no campo do inimigo
                                else
                                {
                                    Configuracoes.RegistrarEvento("Não há cartas para sofrerem seu efeito", Color.white);
                                    Configuracoes.admJogo.efeitoAtual = null;
                                }
                            }
                        }
                        else
                        {
                            Configuracoes.RegistrarEvento("Esta carta já utilizou seu efeito", Color.white);
                        }
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode utilizar efeitos neste turno", Color.white);
                    }
                }
            }
        }
    }
}