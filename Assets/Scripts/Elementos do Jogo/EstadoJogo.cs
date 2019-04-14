using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EstadoJogo", menuName = "EstadoJogo", order = 0)]
public class EstadoJogo : ScriptableObject
{
    public Acao[] acoes;
    public void Tick(float d)
    {
        for (int i = 0; i < acoes.Length; i++)
        {
            acoes[i].Executar(d);
        }
    }
}
