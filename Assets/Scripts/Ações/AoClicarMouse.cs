using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Detectar Clique do Mouse")]
public class AoClicarMouse : Acao
{
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData dadosDoPonto = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> resultados = new List<RaycastResult>();
            EventSystem.current.RaycastAll(dadosDoPonto, resultados);

            foreach (RaycastResult r in resultados)
            {
                IClicavel c = r.gameObject.GetComponentInParent<IClicavel>();
                if (c != null)
                {
                    c.AoClicar();
                    break;
                }
            }
        }
    }
}
