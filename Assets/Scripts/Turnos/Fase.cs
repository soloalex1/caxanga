using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fase : ScriptableObject
{
    public bool forcarSaida;
    public abstract bool FoiCompletada();

    [System.NonSerialized]
    protected bool foiIniciada;

    public abstract void AoIniciarFase();
    
    public abstract void AoEncerrarFase();

}
