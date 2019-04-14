using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaCarta : MonoBehaviour, IClicavel
{
    public LogicaInstanciaCarta logicaAtual;

    void IClicavel.AoClicar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoClicar(this);
        }
    }

    void IClicavel.AoSelecionar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoSelecionar(this);
        }
    }
}
