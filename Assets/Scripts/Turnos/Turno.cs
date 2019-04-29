using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turnos/Turno")]
public class Turno : ScriptableObject
{
    // pra nunca salvar o valor do indice, setei como NonSerialized e deixei o valor padrão como zero
    [System.NonSerialized]
    public int indice = 0;

    public SeguradorDeJogador jogador;
    public VariavelFase faseAtual;
    public Fase[] fases;


    public bool Executar()
    {
        faseAtual.valor = fases[indice];
        fases[indice].AoIniciarFase();

        bool faseFoiEncerrada = fases[indice].FoiCompletada();
        bool resultado = false;

        if (faseFoiEncerrada)
        {
            fases[indice].AoEncerrarFase();
            // aoCompletarFase.Raise();

            indice++;
            if (indice > fases.Length - 1)
            {
                indice = 0;
                resultado = true;
            }
        }

        return resultado;
    }
    public void FinalizarFaseAtual()
    {
        fases[indice].forcarSaida = true;
    }
}
