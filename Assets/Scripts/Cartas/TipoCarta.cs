using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipoCarta", menuName = "Cartas/TipoCarta", order = 0)]
public abstract class TipoCarta : ScriptableObject
{
    public string nomeTipo;

    public abstract void Inicializar(ExibirInfoCarta e);
}