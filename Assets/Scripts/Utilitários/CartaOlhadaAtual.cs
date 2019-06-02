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
        SetPoderECusto();
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
    void SetPoderECusto()
    {
        if (cartaAtual.valor != null)
        {
            cartaAtual.valor.infoCarta.carta.AcharPropriedadePeloNome("Custo").intValor = cartaAtual.valor.custo;
            cartaAtual.valor.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor = cartaAtual.valor.poder;
            if (cartaAtual.valor.protegido)
            {
                cartaAtual.valor.infoCarta.carta.protegido = true;
                infoCarta.protegido = true;
            }
            else
                cartaAtual.valor.infoCarta.carta.protegido = false;

        }
    }
}
