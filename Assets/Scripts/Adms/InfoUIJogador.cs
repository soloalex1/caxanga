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
    public GameObject molduraJogador;
    Image barraDeVida;
    public Sprite spriteBarraDeVida, spriteBarraDeVidaPerdida;
    public Sprite spriteNaoPodeBaixarLenda;
    public Sprite spriteNaoPodeBaixarFeitico;

    public Sprite spritePodeBaixarLenda;
    public Sprite spritePodeBaixarFeitico;

    public void AtualizarTudo()
    {
        AtualizarNomeJogador();
        AtualizarVida();
        AtualizarMagia();
        AtualizarBarraDeVida();
        AtualizarMoldura();
    }

    public void AtualizarMoldura()
    {
        molduraJogador.GetComponent<Image>().sprite = jogador.moldura;
    }
    public void AtualizarBarraDeVida()
    {
        Transform painelBarrasVida = this.gameObject.transform.GetChild(0).GetChild(1);
        Debug.Log(jogador.nomeJogador + " só tem " + jogador.barrasDeVida + " barras");
        for (int i = 1; i <= 3; i++)
        {
            barraDeVida = painelBarrasVida.GetChild(i - 1).GetComponent<Image>();
            barraDeVida.sprite = spriteBarraDeVida;
            if (i > jogador.barrasDeVida)
            {
                barraDeVida.sprite = spriteBarraDeVidaPerdida;
            }
        }
    }

    public void SpritesCartasUtilizaveis()
    {
        foreach (InstanciaCarta c in jogador.cartasMao)
        {
            if (c != null && c.infoCarta != null)
            {
                if (jogador.magia < c.custo)
                {
                    if (c.infoCarta.carta.tipoCarta.nomeTipo == "Lenda")
                    {
                        c.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = spriteNaoPodeBaixarLenda;
                    }
                    else
                    {
                        c.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = spriteNaoPodeBaixarFeitico;
                    }
                }
                else
                {
                    if (c.infoCarta.carta.tipoCarta.nomeTipo == "Lenda")
                    {
                        c.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = spritePodeBaixarLenda;
                    }
                    else
                    {
                        c.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = spritePodeBaixarFeitico;
                    }
                }
            }
        }
    }
    public void AtualizarNomeJogador()
    {
        // nomeJogador.text = jogador.nomeJogador;
        retratoJogador.sprite = jogador.retratoJogador;
    }
    public void AtualizarVida()
    {
        vidaJogador.text = jogador.vida.ToString();
    }
    public void AtualizarMagia()
    {
        magiaJogador.text = jogador.magia.ToString();
        SpritesCartasUtilizaveis();
    }

    public IEnumerator AnimacaoDano(int dano)
    {
        gameObject.transform.Find("Coração Dano").gameObject.SetActive(true);
        transform.Find("Coração Dano").Find("Texto").GetComponent<Text>().text = dano.ToString();
        yield return new WaitForSeconds(0.8f);
        transform.Find("Coração Dano").gameObject.SetActive(false);
    }
    public IEnumerator AnimacaoCura(int cura)
    {
        transform.Find("Coração Cura").gameObject.SetActive(true);
        transform.Find("Coração Cura").Find("Texto").GetComponent<Text>().text = "+" + cura.ToString();
        yield return new WaitForSeconds(0.8f);
        transform.Find("Coração Cura").gameObject.SetActive(false);
    }
}


