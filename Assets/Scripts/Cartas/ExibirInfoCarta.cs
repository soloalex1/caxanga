using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExibirInfoCarta : MonoBehaviour
{
    public Carta carta;
    public ExibirInfoPropriedades[] propriedades;
    public GameObject mostrarPoder, imagemProtegido;
    public TipoFeitico tipoFeitico;
    public TipoLenda tipoLenda;
    public bool protegido;
    public Sprite templateLenda;
    public Sprite templateFeitico;
    public Sprite spritePodeAtacar;

    public Sprite spriteNaoPodeAtacar;
    public void CarregarCarta(Carta c)
    {

        imagemProtegido = transform.Find("Frente da Carta/Protegido").gameObject;
        if (c == null)
            return;
        carta = c;

        InstanciaCarta instCarta = GetComponent<InstanciaCarta>();
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
                if (instCarta != null)
                {
                    if (ep.elemento.name == "Poder")
                    {
                        ep.texto.text = instCarta.poder.ToString();
                    }
                    if (ep.elemento.name == "Custo")
                    {
                        ep.texto.text = instCarta.custo.ToString();
                    }
                }
                ep.texto.gameObject.SetActive(true);
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
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").gameObject.GetComponent<VerticalLayoutGroup>().childControlHeight = true;
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").gameObject.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            // gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Texto Categoria").gameObject.GetComponent<Text>().resizeTextMaxSize = 32;
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Texto Categoria").gameObject.GetComponent<Text>().resizeTextForBestFit = true;


        }
        else
        {
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Linha").gameObject.SetActive(true);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").Find("Efeito").gameObject.SetActive(true);
            gameObject.transform.Find("Frente da Carta").Find("Grid Efeito + Texto").gameObject.GetComponent<VerticalLayoutGroup>().childControlHeight = false;

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
        if (protegido)
        {
            imagemProtegido.SetActive(true);
        }
        else
        {
            imagemProtegido.SetActive(false);
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
