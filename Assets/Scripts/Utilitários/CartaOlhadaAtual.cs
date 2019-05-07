using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaOlhadaAtual : MonoBehaviour
{

    public VariavelCarta cartaAtual;
    public ExibirInfoCarta infoCarta;
    Vector3 posicao;
    public void CarregarCartaOlhada()
    {
        infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
        infoCarta.gameObject.SetActive(true);
    }
    public void FecharCarta()
    {
        infoCarta.gameObject.SetActive(false);
    }
    private void Start()
    {
        Vector3 posicao = Vector3.zero;
        FecharCarta();
    }
}
