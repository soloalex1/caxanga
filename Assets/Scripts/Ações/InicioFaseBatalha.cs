using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ações/InicioFaseBatalha")]
public class InicioFaseBatalha : Condicao
{
    public override bool condicaoValida()
    {
        AdmJogo admJogo = AdmJogo.singleton;
        if (admJogo.jogadorAtual.cartasBaixadas.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
