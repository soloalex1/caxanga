using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Áreas/Baixar Carta Quando Segurando")]
public class LogicaBaixarCartaArea : LogicaArea
{

    public VariavelCarta cartaAtual;
    public TipoCarta tipoCarta;

    public VariavelTransform gridArea;

    public override void Executar()
    {
        if(cartaAtual.valor != null)
        {
            if(cartaAtual.valor.infoCarta.carta.tipoCarta == tipoCarta)
            {
                // Baixa a carta no campo

                Debug.Log("baixou a carta no campo");
                cartaAtual.valor.transform.SetParent(gridArea.valor.transform);
            }
        }
    }
}
