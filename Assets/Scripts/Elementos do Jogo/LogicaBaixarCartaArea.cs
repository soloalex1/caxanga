using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Áreas/Baixar Carta Quando Segurando")]
public class LogicaBaixarCartaArea : LogicaArea
{

    public VariavelCarta cartaAtual;
    public TipoCarta tipoLenda;
    public TipoCarta tipoFeitico;

    public VariavelTransform gridArea;

    public LogicaInstanciaCarta logicaCartaBaixa;

    public override void Executar()
    {
        if (cartaAtual.valor != null)
        {

            Carta c = cartaAtual.valor.infoCarta.carta;
            if (cartaAtual.valor.infoCarta.carta.tipoCarta == tipoLenda)
            {
                bool podeUsarCarta = Configuracoes.admJogo.jogadorAtual.PodeUsarCarta(c);
                if (podeUsarCarta) //pode baixar carta
                {
                    //define o pai da carta para ser o grid lá do Cartas Baixadas
                    Configuracoes.DefinirPaiCarta(cartaAtual.valor.transform, gridArea.valor.transform);
                    cartaAtual.valor.logicaAtual = logicaCartaBaixa;

                }
                // Dá um SetActive() pra sobrescrever o que tem no SelecaoAtual
                cartaAtual.valor.gameObject.SetActive(true);

            }

        }
    }
}
