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
                // Baixa a carta no campo, transferindo do parent atual (a mão) pro campo (novo parent)
                cartaAtual.valor.transform.SetParent(gridArea.valor.transform);

                // Seta a escala para um tamanho que ficou bom no tabuleiro, porque tava quebrando antes
                // Se precisar, pode mudar, não faço ideia do que mais pode ser feito pra deixar direito
                cartaAtual.valor.transform.localScale = new Vector3(0.3F, 0.3F, 0.3F);

                // Zera o posicionamento da carta, pra mostrar em tela sem quebrar tudo
                cartaAtual.valor.transform.localPosition = Vector3.zero;

                // Ajusta a rotação em relação ao novo parent, pra manter a perspectiva
                cartaAtual.valor.transform.localEulerAngles = Vector3.zero;

                // Dá um SetActive() pra sobrescrever o que tem no SelecaoAtual
                cartaAtual.valor.gameObject.SetActive(true);

                cartaAtual.valor.logicaAtual = logicaCartaBaixa;
            }
        }
    }
}
