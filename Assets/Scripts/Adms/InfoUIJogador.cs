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
    public void AtualizarTudo()
    {
        AtualizarNomeJogador();
        AtualizarVida();
        AtualizarMagia();
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


