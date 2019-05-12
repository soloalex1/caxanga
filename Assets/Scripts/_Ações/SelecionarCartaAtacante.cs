using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Selecionar Carta Atacante")]
public class SelecionarCartaAtacante : Acao
{
    public GameEvent cartaAtacou;
    public EstadoJogador atacando;
    public EstadoJogador faseDeBatalha;
    public VariavelTransform gridAreaDropavel;
    public override void Executar(float d)
    {
        if (Configuracoes.admJogo.estadoAtual != atacando)
        {
            Configuracoes.admJogo.DefinirEstado(faseDeBatalha);
        }
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
                    Configuracoes.RegistrarEvento("A Carta " + instCarta.infoCarta.carta.name + " foi selecionada para atacar", Color.white);
                    if (gridAreaDropavel != null)
                    {
                        gridAreaDropavel.valor.GetComponent<Image>().raycastTarget = false;
                    }
                    if (instCarta.efeito != null && instCarta.efeito.eventoAtivador == cartaAtacou)
                    {
                        Configuracoes.admJogo.StartCoroutine("ExecutarEfeito", instCarta.efeito);
                    }
                    Configuracoes.admJogo.DefinirEstado(atacando);
                    Configuracoes.admJogo.cartaAtacante = instCarta;
                }
                else
                {
                    Configuracoes.RegistrarEvento("A Carta " + instCarta.infoCarta.carta.name + " não pode atacar neste turno", Color.white);
                }
            }
        }
    }
}
