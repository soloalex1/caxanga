using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ElementosJogo/CartaBaixada")]
public class CartaBaixada : LogicaInstanciaCarta
{
    public override void AoClicar(InstanciaCarta c)
    {
        Debug.Log("Esta carta está no campo");
    }
    public override void AoSelecionar(InstanciaCarta c)
    {

    }
}