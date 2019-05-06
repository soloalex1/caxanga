using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EstadoJogador", menuName = "EstadoJogador", order = 0)]
public class EstadoJogador : ScriptableObject
{
    public TipoFeitico tipoFeitico;
    public TipoLenda tipoLenda;

    public Acao[] acoes;
    public void Tick(float d)
    {
        for (int i = 0; i < acoes.Length; i++)
        {
            acoes[i].Executar(d);
        }
    }
}
