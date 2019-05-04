using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Selecionar Carta Atacada")]
public class SelecionarCartaAtacada : Acao
{
    public EstadoJogador faseDeBatalha;
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
                }
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
                }
            }
        }
    }
}
