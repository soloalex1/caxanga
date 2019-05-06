using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seguradores/Segurador de Cartas")]
public class SeguradorDeCartas : ScriptableObject
{
    public VariavelTransform gridMao;
    public VariavelTransform gridCartasBaixadas;
    public VariavelTransform gridCemiterio;

    public void CarregarCartasJogador(SeguradorDeJogador seguradorJogador, InfoUIJogador InfoUIJogador)
    {
        foreach (InstanciaCarta c in seguradorJogador.cartasBaixadas)
        {
            Configuracoes.DefinirPaiCarta(c.infoCarta.gameObject.transform, gridCartasBaixadas.valor.transform);
        }
        foreach (InstanciaCarta c in seguradorJogador.cartasMao)
        {
            Configuracoes.DefinirPaiCarta(c.infoCarta.gameObject.transform, gridMao.valor.transform);
        }
        seguradorJogador.infoUI = InfoUIJogador;
        seguradorJogador.CarregarInfoUIJogador();
    }

    public void CarregarCemiterio(SeguradorDeJogador jogador)
    {
        gridCemiterio.valor.transform.SetParent(jogador.variavelCemiterio.valor);
        Debug.Log(jogador.nomeJogador + " recebeu cemitério");
    }
}
