using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/SegurarMouseComCarta")]
public class SegurarMouseComCarta : Acao
{
    //Essa classe é um tipo de ação. Nela, definimos o que o jogador pode fazer quando estiver no estado 'SEGURANDO CARTA'

    public VariavelCarta cartaAtual;
    public GameEvent aoJogarCartaDaMao;
    public EstadoJogador controladorEstadoJogador;//variável para poder controlar o estado do jogador
    public VariavelTransform gridAreaDropavel;
    //Como todas as ações, precisamos implementar o Executar(), que vai ser chamado quando o jogador mudar de estado
    public override void Executar(float d)
    {
        bool btMouseApertado = Input.GetMouseButton(0);
        if (gridAreaDropavel != null)
        {
            gridAreaDropavel.valor.gameObject.GetComponent<Image>().color = new Color(0, 0.8F, 0, 0.5F);
            gridAreaDropavel.valor.gameObject.GetComponent<Image>().raycastTarget = true;
        }

        if (btMouseApertado == false)//se o jogador não estiver apertando o botão do mouse
        {
            gridAreaDropavel.valor.gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                //procurando por áreas em que o jogador pode jogar uma carta

                Area a = r.gameObject.GetComponentInParent<Area>();
                if (a != null)
                {
                    a.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
                    a.AoDropar();
                    gridAreaDropavel.valor.gameObject.GetComponent<Image>().raycastTarget = false;
                    break;
                }
            }
            gridAreaDropavel.valor.gameObject.GetComponent<Image>().raycastTarget = false;
            cartaAtual.valor.gameObject.SetActive(true);
            cartaAtual.valor = null;
            if (Configuracoes.admJogo.estadoAtual.name != "Usando Efeito")
            {
                Configuracoes.admJogo.DefinirEstado(controladorEstadoJogador);
            }
            aoJogarCartaDaMao.Raise();
            return;
        }
    }
}
