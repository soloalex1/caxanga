using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LogicaInstanciaCarta : ScriptableObject
{
    public abstract void AoClicar(InstanciaCarta c);
    public abstract void AoOlhar(InstanciaCarta c);
}