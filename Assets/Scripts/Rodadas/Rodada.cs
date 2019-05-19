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
        Debug.Log(jogador.name);

        for (int j = 0; j < numCartasPuxadasInicioRodada; j++)
        {
            Configuracoes.admJogo.PuxarCarta(jogador);
        }

        // foreach (InstanciaCarta c in jogador.cartasMao)
        // {
        //     if (jogador == Configuracoes.admJogo.jogadorInimigo)
        //     {
        //         c.transform.Find("Fundo da Carta").gameObject.SetActive(true);
        //     }
        //     else
        //     {
        //         c.transform.Find("Fundo da Carta").gameObject.SetActive(false);
        //     }
        //     if (c.transform.Find("Fundo da Carta").gameObject.activeSelf)
        //     {
        //         Debug.Log("Tá escondida");
        //     }
        //     else
        //     {
        //         Debug.Log("Tá mostrando");
        //     }
        // }
    }
    public void PassarRodada()
    {
        jogador.passouRodada = true;
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").SetActive(true);
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").GetComponent<Text>().color = jogador.corJogador;
    }
}
