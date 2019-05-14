using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoAtual : MonoBehaviour
{

    public VariavelCarta cartaAtual;
    public ExibirInfoCarta infoCarta;
    Transform mTransform;
    public void CarregarCartaOlhada()
    {
        infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
        infoCarta.gameObject.SetActive(true);
    }
    public void CarregarCarta()
    {
        cartaAtual.valor.gameObject.SetActive(false);
        infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
        infoCarta.gameObject.SetActive(true);
    }
    public void FecharCarta()
    {
        infoCarta.gameObject.SetActive(false);
    }
    private void Start()
    {
        FecharCarta();
        mTransform = this.transform;
    }
    void Update()
    {
        mTransform.position = Input.mousePosition;
    }
}
