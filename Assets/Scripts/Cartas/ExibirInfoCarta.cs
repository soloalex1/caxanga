using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExibirInfoCarta : MonoBehaviour
{
    public Carta carta;
    public InstanciaCarta instCarta;
    public ExibirInfoPropriedades[] propriedades;
    public GameObject mostrarPoder;
    public TipoFeitico tipoFeitico;
    public TipoLenda tipoLenda;

    public Sprite templateLenda;
    public Sprite templateFeitico;
    public Sprite spritePodeAtacar;

    public Sprite spriteNaoPodeAtacar;

    public void CarregarCarta(Carta c)
    {
        if (c == null)
            return;
        carta = c;


        c.tipoCarta.Inicializar(this);

        FecharPropsIndefinidas();

        for (int i = 0; i < propriedades.Length; i++)
        {
            Propriedades p = c.propriedades[i];

            ExibirInfoPropriedades ep = GetPropriedade(p.elemento);

            if (ep == null)
                continue;

            if (ep.elemento is ElementoNum)
            {
                ep.texto.text = p.intValor.ToString();
                ep.texto.gameObject.SetActive(true);
                Outline contorno = ep.texto.gameObject.GetComponent<Outline>();
                if (contorno != null)
                {
                    contorno.effectColor = Color.black;
                    contorno.effectDistance.Set(2, 2);
                }
            }
            else if (p.elemento is ElementoTexto)
            {
                ep.texto.text = p.stringValor;
                ep.texto.gameObject.SetActive(true);
            }
            else if (p.elemento is ElementoImagem)
            {
                ep.imagem.sprite = p.sprite;
                ep.imagem.gameObject.SetActive(true);
            }
        }

        if (carta.efeito == null)
        {
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Linha").gameObject.SetActive(false);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Efeito").gameObject.SetActive(false);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").gameObject.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            // gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Texto Categoria").gameObject.GetComponent<Text>().resizeTextMaxSize = 32;
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Texto Categoria").gameObject.GetComponent<Text>().resizeTextForBestFit = true;
            

        }
        else
        {
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Linha").gameObject.SetActive(true);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Efeito").gameObject.SetActive(true);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").gameObject.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.UpperCenter;
        }
        if (carta.tipoCarta == tipoFeitico)
        {
            this.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = templateFeitico;
        }
        else
        {
            this.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = templateLenda;
            if (instCarta != null && instCarta.podeAtacarNesteTurno == false)
            {
                this.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = spriteNaoPodeAtacar;
            }
        }

    }

    public void FecharPropsIndefinidas()
    {
        foreach (ExibirInfoPropriedades e in propriedades)
        {
            if (e.imagem != null)
                e.imagem.gameObject.SetActive(false);
            if (e.texto != null)
                e.texto.gameObject.SetActive(false);
        }
    }
    public ExibirInfoPropriedades GetPropriedade(Elemento e)
    {
        ExibirInfoPropriedades resultado = null;

        for (int i = 0; i < propriedades.Length; i++)
        {
            if (propriedades[i].elemento == e)
            {
                resultado = propriedades[i];
                break;
            }
        }

        return resultado;
    }
}
