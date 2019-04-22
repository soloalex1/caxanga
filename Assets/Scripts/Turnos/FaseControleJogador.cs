using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Turnos/Fase de Controle [Jogador]")]
public class FaseControleJogador : Fase
{
    public EstadoJogador estadoControleJogador;
    
    // método a ser executado quando a fase é completada
    public override bool FoiCompletada()
    {
        if(forcarSaida)
        {
            forcarSaida = false;
            return true;
        } 

        return false;

    }

    public override void AoIniciarFase()
    {
        if(!foiIniciada)
        {
            Configuracoes.admJogo.DefinirEstado(estadoControleJogador);
            Configuracoes.admJogo.aoMudarFase.Raise();
            foiIniciada = true;
        }

    }

    public override void AoEncerrarFase()
    {
        if(foiIniciada)
        {
            Configuracoes.admJogo.DefinirEstado(null);
            foiIniciada = false;
        }
    }
}
