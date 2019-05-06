using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Ações/Ativar Efeito Carta")]
public class AtivarEfeitoCarta : Acao
{
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

                    if (jogadorAtual.podeUsarEfeito && !instCarta.efeitoUsado)
                    {
                        if (instCarta.efeito.podeUsarEmSi == true)
                        {
                            Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
                            if (Configuracoes.admJogo.efeitoAtual.modoEfeito == "")
                            {
                                Configuracoes.RegistrarEvento("Esta carta não tem efeito", Color.white);
                                return;
                            }
                            Configuracoes.RegistrarEvento("A carta " + instCarta.infoCarta.carta.name + " vai usar o seu efeito", Color.white);
                            return;
                        }
                        else
                        {
                            if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Count > 0)
                            {
                                Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
                                Configuracoes.RegistrarEvento("A carta " + instCarta.infoCarta.carta.name + " vai usar o seu efeito", Color.white);
                                return;
                            }
                            else
                            {
                                //Debug.Log("Não há cartas para sofrerem seu efeito");
                            }
                        }
                        // Configuracoes.RegistrarEvento("Selecione um alvo para usar o seu efeito", Color.white);
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Esta carta já utilizou seu efeito", Color.white);
                    }
                }
            }
        }
    }
}