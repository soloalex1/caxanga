using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turnos/Fase de Batalha [Jogador]")]
public class FaseBatalha : Fase
{
    public Condicao condicaoBatalhaPossivel;
    public EstadoJogador emFaseBatalha;
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
            forcarSaida = !condicaoBatalhaPossivel.condicaoValida(); //se a batalha não for possível, força a saída
            Configuracoes.admJogo.DefinirEstado((!forcarSaida) ? emFaseBatalha : null);
            Configuracoes.admJogo.aoMudarFase.Raise();
            Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno = 0;
            Configuracoes.admJogo.jogadorAtual.feiticosBaixadosNoTurno = 0;
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
