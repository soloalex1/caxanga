using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Selecionar Carta Atacante")]
public class SelecionarCartaAtacante : Acao
{
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                SeguradorDeJogador jogador = Configuracoes.admJogo.jogadorAtual;
                InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (!jogador.cartasBaixadas.Contains(instCarta))
                    return;
                if (instCarta.podeAtacarNesteTurno)
                {
                    Debug.Log("A carta " + instCarta.name + " pode e vai atacar");
                }
            }
        }
    }
}
