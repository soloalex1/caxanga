using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Turnos/Turno")]
public class Turno : ScriptableObject
{
    // pra nunca salvar o valor do index, setei como NonSerialized e deixei o valor padrão como zero
    [System.NonSerialized]
    public int index = 0;
    public Fase[] fases;
    

    public bool Executar()
    {
        fases[index].AoIniciarFase();

        bool faseFoiEncerrada = fases[index].FoiCompletada();
        bool resultado = false;

        if(faseFoiEncerrada)
        {
            fases[index].AoEncerrarFase();
            index++;
            if(index > fases.Length - 1)
            {
                index = 0;
                resultado = true;
            }
        }

        return resultado;
    }
}
