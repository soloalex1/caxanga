using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Detectar Mouse Sobre")]
public class DetectarMouseSobre : Acao
{
    public override void Executar(float d)
    {
        List<RaycastResult> resultados = Configuracoes.GetUIObjs();
        IClicavel c = null;

        foreach (RaycastResult r in resultados)
        {
            c = r.gameObject.GetComponentInParent<InstanciaCarta>();
            if (c != null)
            {
                c.AoSelecionar();
                break;
            }
        }

    }
}
