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
            cartasOponenteViradas = false;
            Configuracoes.admJogo.DefinirEstado(estadoControleJogador);
            Configuracoes.admJogo.aoMudarFase.Raise();
            Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno = 0;

            if (!cartasOponenteViradas)
            {

                foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasMao)
                {
                    c.transform.Find("Fundo da Carta").gameObject.SetActive(true);
                }
                foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorAtual.cartasMao)
                {
                    c.transform.Find("Fundo da Carta").gameObject.SetActive(false);
                }
                cartasOponenteViradas = true;
            }
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
