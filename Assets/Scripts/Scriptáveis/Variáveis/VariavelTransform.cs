using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variáveis/Variável de Transform")]
public class VariavelTransform : ScriptableObject
{

    public Transform valor;

            public void Set(Transform v)
            {
                valor = v;
            }

            public void Set(VariavelTransform v)
            {
                valor = v.valor;
            }

            public void Clear()
            {
                valor = null;
    }
}
