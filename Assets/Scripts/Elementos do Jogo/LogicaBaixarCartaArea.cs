using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Áreas/Baixar Carta Quando Segurando")]
public class LogicaBaixarCartaArea : LogicaArea
{

    public VariavelCarta cartaAtual;
    public TipoCarta tipoCarta;

    public VariavelTransform gridArea;

    public LogicaInstanciaCarta logicaCartaBaixa;

    public override void Executar()
    {
        if (cartaAtual.valor != null)
        {
            if (cartaAtual.valor.infoCarta.carta.tipoCarta == tipoCarta)
            {
                //define o pai da carta para ser o grid lá do Cartas Baixadas
                Configuracoes.DefinirPaiCarta(cartaAtual.valor.transform, gridArea.valor.transform);
                // Dá um SetActive() pra sobrescrever o que tem no SelecaoAtual
                cartaAtual.valor.gameObject.SetActive(true);

                cartaAtual.valor.logicaAtual = logicaCartaBaixa;
            }
        }
    }
}
