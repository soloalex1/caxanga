using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turnos/Fase de Controle [Jogador]")]
public class FaseControleJogador : Fase
{
    public EstadoJogador estadoControleJogador;
    public bool cartasOponenteViradas;
    // método a ser executado quando a fase é completada
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
            Configuracoes.admJogo.DefinirEstado(estadoControleJogador);
            Configuracoes.admJogo.aoMudarFase.Raise();
            foiIniciada = true;
            int cont = 0;
            foreach (InstanciaCarta carta in Configuracoes.admJogo.jogadorAtual.cartasMao)
            {
                if (Configuracoes.admJogo.jogadorAtual.magia > carta.infoCarta.carta.AcharPropriedadePeloNome("Custo").intValor)
                {
                    cont++;
                }
            }
            if (cont == 0)
            {
                Configuracoes.admJogo.jogadorAtual.passouTurno = true;
            }
            cont = 0;
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
