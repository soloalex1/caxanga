using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExibirInfoCarta : MonoBehaviour
{
    public Carta carta;
    public ExibirInfoPropriedades[] propriedades;
    public GameObject mostrarPoder;
    private void Start()
    {
        CarregarCarta(carta);
    }
    public void CarregarCarta(Carta c)
    {
        if (c == null)
            return;
        carta = c;

        c.tipoCarta.Inicializar(this);

        for (int i = 0; i < propriedades.Length; i++)
        {
            Propriedades p = c.propriedades[i];

            ExibirInfoPropriedades ep = GetPropriedade(p.elemento);

            if (ep == null)
                continue;

            if (ep.elemento is ElementoNum)
            {
                ep.texto.text = p.intValor.ToString();
            }
            else if (p.elemento is ElementoTexto)
            {
                ep.texto.text = p.stringValor;
            }
            else if (p.elemento is ElementoImagem)
            {
                ep.imagem.sprite = p.sprite;
            }
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
