using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Selecionar Carta Atacada")]
public class SelecionarCartaAtacada : Acao
{
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                SeguradorDeJogador jogadorInimigo = null;
                if (Configuracoes.admJogo.jogadorAtual == Configuracoes.admJogo.todosJogadores[0])
                {
                    jogadorInimigo = Configuracoes.admJogo.todosJogadores[1];
                }
                else
                {
                    jogadorInimigo = Configuracoes.admJogo.todosJogadores[0];
                }
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (!jogadorInimigo.cartasBaixadas.Contains(instCarta))
                    return;
                if (instCarta.podeSerAtacada)
                {
                    Configuracoes.admJogo.cartaAtacada = instCarta;
                    Configuracoes.RegistrarEvento("O alvo " + instCarta.infoCarta.carta.name + " foi selecionado para ser atacado", Color.white);
                }
            }
        }
    }
}
