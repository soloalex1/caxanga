using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variáveis/Variável de String")]
public class VariavelString : ScriptableObject
{
    public string valor;

    public void Set(string v)
    {
        valor = v;
    }

    public void Set(VariavelString v)
    {
        valor = v.valor;
    }

    public bool VaziaOuNula()
    {
        return string.IsNullOrEmpty(valor);
    }

    public void Limpar()
    {
        valor = string.Empty;
    }
}