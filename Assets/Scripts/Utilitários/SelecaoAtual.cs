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
        if(cartaAtual != null)
        {
            // também não sei
            cartaAtual.valor.gameObject.SetActive(false);
            infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
            infoCarta.gameObject.SetActive(true);
        }

    }

    private void Start()
    {
        mTransform = this.transform;
    }

    void Update()
    {
        mTransform.position = Input.mousePosition;
    }
}
