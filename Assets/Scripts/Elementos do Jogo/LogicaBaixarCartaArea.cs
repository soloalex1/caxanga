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

            bool podeUsarCarta = Configuracoes.admJogo.jogadorAtual.PodeUsarCarta(c);
            if (podeUsarCarta)
            {
                if (cartaAtual.valor.infoCarta.carta.tipoCarta == tipoLenda)
                {
                    if (Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno < Configuracoes.admJogo.jogadorAtual.maxLendasTurno) //pode baixar carta
                    {
                        //define o pai da carta para ser o grid lá do Cartas Baixadas
                        Configuracoes.BaixarCartaLenda(cartaAtual.valor.transform, gridArea.valor.transform, cartaAtual.valor);
                        cartaAtual.valor.logicaAtual = logicaCartaBaixa;
                        Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno++;
                        cartaAtual.valor.gameObject.SetActive(true);
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode baixar mais de uma Lenda por turno", Color.white);
                    }
                }
                if (cartaAtual.valor.infoCarta.carta.tipoCarta == tipoFeitico)
                {
                    if (Configuracoes.admJogo.jogadorAtual.feiticosBaixadosNoTurno < Configuracoes.admJogo.jogadorAtual.maxFeiticosTurno)
                    {
                        Configuracoes.admJogo.efeitoAtual = cartaAtual.valor.infoCarta.carta.efeito;
                        Configuracoes.RegistrarEvento("Escolha um alvo para o efeito de " + cartaAtual.valor.infoCarta.carta.name, Color.white);
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode baixar mais de um Feitiço por turno", Color.white);
                    }
                }
            }
            else
            {
                //não pode usar cartas essa rodada
            }
            // Dá um SetActive() pra sobrescrever o que tem no SelecaoAtual
            cartaAtual.valor.gameObject.SetActive(true);

        }

    }
}
