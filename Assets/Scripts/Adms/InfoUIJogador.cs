using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUIJogador : MonoBehaviour
{
    public SeguradorDeJogador jogador;
    public Image retratoJogador;
    public Text vidaJogador;
    public Text magiaJogador;
    public Text nomeJogador;
    Image barraDeVida;
    public void AtualizarTudo()
    {
        AtualizarNomeJogador();
        AtualizarVida();
        AtualizarMagia();
        AtualizarBarraDeVida();
    }
    public void AtualizarBarraDeVida()
    {
        int cont = 1;
        Transform painelBarrasVida = this.gameObject.transform.GetChild(0).GetChild(3);
        for (int i = 1; i <= 3; i++)
        {
            barraDeVida = painelBarrasVida.Find("Barra de vida " + i).GetComponent<Image>();
            if (cont <= jogador.barrasDeVida)
            {
                barraDeVida.color = new Color(1, 1, 1, 1);
                cont++;
            }
        }
    }

    public void AtualizarNomeJogador()
    {
        nomeJogador.text = jogador.nomeJogador;
        retratoJogador.sprite = jogador.retratoJogador;
    }
    public void AtualizarVida()
    {
        vidaJogador.text = "Vida: " + jogador.vida.ToString();
    }
    public void AtualizarMagia()
    {
        magiaJogador.text = "Magia: " + jogador.magia.ToString();
    }
}


