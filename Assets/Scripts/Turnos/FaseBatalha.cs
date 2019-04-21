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

    }

    public override void AoEncerrarFase()
    {

    }
}
