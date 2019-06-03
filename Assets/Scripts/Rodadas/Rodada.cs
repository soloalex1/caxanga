using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Rodadas/Rodada")]
public class Rodada : ScriptableObject
{
    public bool terminou;
    public Turno turno;
    public SeguradorDeJogador jogador;

    public int magiaAumentadaPorRodada;
    public int numCartasPuxadasInicioRodada;

    public void IniciarRodada()
    {

        //redefinir jogadores
        jogador.magia = jogador.magiaInicial + (2 * Configuracoes.admJogo.rodadaAtual);
        jogador.vida = jogador.vidaInicial;
        jogador.lendasBaixadasNoTurno = 0;
        jogador.feiticosBaixadosNoTurno = 0;
        jogador.podeSerAtacado = true;
        jogador.podeUsarEfeito = true;
        jogador.passouRodada = false;
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").SetActive(false);

        jogador.CarregarInfoUIJogador();
        if (jogador.cartasBaixadas.Count > 0)
        {
            foreach (InstanciaCarta carta in jogador.cartasBaixadas)
            {
                if (jogador.cartasBaixadas.Contains(carta))
                {
                    Configuracoes.admJogo.MatarCarta(carta, jogador);
                }
                if (jogador.cartasBaixadas.Count == 0)
                {
                    break;
                }
            }
        }

        for (int j = 0; j < numCartasPuxadasInicioRodada; j++)
        {
            Configuracoes.admJogo.PuxarCarta(jogador);
        }
    }
    public void PassarRodada()
    {
        jogador.passouRodada = true;
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").SetActive(true);
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").GetComponent<Text>().color = jogador.corJogador;
    }
}
