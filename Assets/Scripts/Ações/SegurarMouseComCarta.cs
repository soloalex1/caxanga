using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/SegurarMouseComCarta")]
public class SegurarMouseComCarta : Acao
{
    //Essa classe é um tipo de ação. Nela, definimos o que o jogador pode fazer quando estiver no estado 'SEGURANDO CARTA'

    public VariavelCarta cartaAtual;
    public GameEvent aoControlarEstadoJogador;
    public EstadoJogador controladorEstadoJogador;//variável para poder controlar o estado do jogador

    //Como todas as ações, precisamos implementar o Executar(), que vai ser chamado quando o jogador mudar de estado
    public override void Executar(float d)
    {
        bool btMouseApertado = Input.GetMouseButton(0);

        if (btMouseApertado == false)//se o jogador não estiver apertando o botão do mouse
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();

            bool foiDropadoNaArea = false;

            foreach (RaycastResult r in resultados)
            {
                //procurando por áreas em que o jogador pode jogar uma carta

                Area a = r.gameObject.GetComponentInParent<Area>();
                if(a != null)
                {
                    foiDropadoNaArea = true;
                    a.AoDropar();
                }
            }

            if(!foiDropadoNaArea)
            {
                cartaAtual.valor.gameObject.SetActive(true);
            }
            else 
            {
                cartaAtual.valor = null;
            }

            Configuracoes.admJogo.DefinirEstado(controladorEstadoJogador);
            aoControlarEstadoJogador.Raise();
            return;
        }
    }
}
