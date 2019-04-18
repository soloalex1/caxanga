using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipoLenda", menuName = "Cartas/TipoCarta/Lenda")]
public class TipoLenda : TipoCarta
{
    public override void Inicializar(ExibirInfoCarta e)
    {
        base.Inicializar(e);
        e.mostrarPoder.SetActive(true);
    }
}
