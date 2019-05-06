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
        foreach (InstanciaCarta c in seguradorJogador.cartasCemiterio)
        {

            Configuracoes.DefinirPaiCarta(c.infoCarta.gameObject.transform, gridCemiterio.valor.transform);
            Vector3 posicao = Vector3.zero;
            posicao.x = seguradorJogador.cartasCemiterio.Count * 10;
            posicao.z = seguradorJogador.cartasCemiterio.Count * 10;

            c.transform.localPosition = posicao;
            c.transform.localRotation = Quaternion.identity;
            c.transform.localScale = Vector3.one;

        }

        seguradorJogador.infoUI = InfoUIJogador;
        seguradorJogador.CarregarInfoUIJogador();
    }
}
