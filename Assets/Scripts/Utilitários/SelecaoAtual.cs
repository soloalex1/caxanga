using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelecaoAtual : MonoBehaviour
{

    public VariavelCarta cartaAtual;
    public ExibirInfoCarta infoCarta;
    Transform mTransform;

    public void CarregarCarta()
    {
        cartaAtual.valor.gameObject.SetActive(false);
        infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
        infoCarta.gameObject.SetActive(true);

    }

    private void Start()
    {
        infoCarta.gameObject.SetActive(false);
        mTransform = this.transform;
    }

    void Update()
    {
        mTransform.position = Input.mousePosition;
    }
}
