using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ElementosJogo/CartaMao")]
public class CartaMao : LogicaInstanciaCarta
{
    public override void AoClicar(InstanciaCarta c)
    {
        Debug.Log("Esta carta está na minha mão");
    }
    public override void AoSelecionar(InstanciaCarta c)
    {

    }
}
