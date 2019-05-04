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
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {
                IClicavel c = r.gameObject.GetComponentInParent<IClicavel>();
                if (c != null)
                {
                    InstanciaCarta instCarta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                    if (Configuracoes.admJogo.jogadorAtual.cartasMao.Contains(instCarta))
                    {
                        c.AoClicar();
                    }
                    break;
                }
            }
        }
    }
}
