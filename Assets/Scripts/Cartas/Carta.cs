using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Nova Carta")]
public class Carta : ScriptableObject
{
    public TipoCarta tipoCarta;
    public Propriedades[] propriedades;

}
