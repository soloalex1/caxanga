using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Nova Carta")]
public class Carta : ScriptableObject
{
    public TipoCarta tipoCarta;
    public Propriedades[] propriedades;
    public Efeito efeito;
    public bool protegido;
    public Propriedades AcharPropriedadePeloNome(string nomePropriedade)
    {
        foreach (Propriedades p in propriedades)
        {
            if (p != null)
            {
                if (p.elemento.name == nomePropriedade)
                {
                    return p;
                }
            }
        }
        return null;
    }
}
