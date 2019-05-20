using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rodadas/Turno")]
public class Turno : ScriptableObject
{
    bool terminou = false;
    public SeguradorDeJogador jogador;
    public AcaoJogador[] acoesIniciais;
    public void IniciarTurno()
    {
        jogador.fezAlgumaAcao = false;
        terminou = false;
        jogador.lendasBaixadasNoTurno = 0;
        jogador.feiticosBaixadosNoTurno = 0;
        if (jogador.protegido == false)
        {
            jogador.podeSerAtacado = true;
        }
        if (jogador.silenciado == false)
        {
            jogador.podeUsarEfeito = true;
        }

        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.FadeTextoTurno(jogador));

        if (acoesIniciais == null)
            return;
        for (int i = 0; i < acoesIniciais.Length; i++)
        {
            acoesIniciais[i].Executar(jogador);
        }
    }
    public void FinalizarTurno()
    {
        terminou = true;
    }
}
