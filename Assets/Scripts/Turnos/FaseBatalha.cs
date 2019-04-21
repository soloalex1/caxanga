using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Turnos/Fase de Batalha [Jogador]")]
public class FaseBatalha : Fase
{
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
            Configuracoes.admJogo.DefinirEstado(null);
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
