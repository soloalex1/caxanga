using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turnos/Fase de Batalha [Jogador]")]
public class FaseBatalha : Fase
{
    public Condicao batalhaPossivel;
    public EstadoJogador controladorFaseBatalha;
    public override bool FoiCompletada()
    {
        if (forcarSaida)
        {
            forcarSaida = false;
            return true;
        }
        return false;
    }

    public override void AoIniciarFase()
    {
        if (!foiIniciada)
        {
            forcarSaida = !batalhaPossivel.condicaoValida(); //se a batalha não for possível, força a saída
            Configuracoes.admJogo.DefinirEstado((!forcarSaida) ? controladorFaseBatalha : null);
            Configuracoes.admJogo.aoMudarFase.Raise();
            foiIniciada = true;
        }
    }

    public override void AoEncerrarFase()
    {
        if (foiIniciada)
        {
            Configuracoes.admJogo.DefinirEstado(null);
            foiIniciada = false;
        }
    }
}
