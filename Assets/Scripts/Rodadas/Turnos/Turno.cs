using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rodadas/Turno")]
public class Turno : ScriptableObject
{
    bool terminou;
    public SeguradorDeJogador jogador;
    public AcaoJogador[] acoesIniciais;
    public void IniciarTurno()
    {
        jogador.fezAlgumaAcao = false;
        terminou = false;
        jogador.lendasBaixadasNoTurno = 0;
        jogador.feiticosBaixadosNoTurno = 0;
        jogador.protegido = false;
        jogador.podeSerAtacado = true;
        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.FadeTextoTurno(jogador));

        if (acoesIniciais == null)
            return;
        for (int i = 0; i < acoesIniciais.Length; i++)
        {
            acoesIniciais[i].Executar(jogador);
        }
        foreach (InstanciaCarta c in jogador.cartasBaixadas)
        {
            c.protegido = false;
            c.podeSofrerEfeito = true;
            c.podeSerAtacada = true;
            c.infoCarta.protegido = false;
            c.infoCarta.CarregarCarta(c.infoCarta.carta);
        }
    }
    public void FinalizarTurno()
    {
        if (jogador.silenciado)
        {
            jogador.silenciado = false;
            jogador.podeUsarEfeito = true;
        }
    }
}
