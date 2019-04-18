using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Acao", menuName = "Ações/Nova Ação")]
public abstract class Acao : ScriptableObject
{
    public abstract void Executar(float d);
}