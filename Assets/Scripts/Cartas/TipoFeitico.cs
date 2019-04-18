using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TipoLenda", menuName = "Cartas/TipoCarta/Feitiço")]
public class TipoFeitico : TipoCarta
{
    public override void Inicializar(ExibirInfoCarta e)
    {
        base.Inicializar(e);
        e.mostrarPoder.SetActive(false);
    }
}
