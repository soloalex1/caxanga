using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Áreas/Baixar Carta Quando Segurando")]
public class LogicaBaixarCartaArea : LogicaArea
{
    public Sprite cursorAlvoCinza;
    public VariavelCarta cartaAtual;
    public VariavelTransform gridArea;
    public EstadoJogador usandoEfeito;
    public LogicaInstanciaCarta logicaCartaBaixa;

    public override void Executar()
    {
        if (cartaAtual.valor != null)
        {

            Carta c = cartaAtual.valor.infoCarta.carta;

            bool podeUsarCarta = Configuracoes.admJogo.jogadorAtual.PodeUsarCarta(c);
            if (podeUsarCarta)
            {
                if (cartaAtual.valor.infoCarta.carta.tipoCarta.nomeTipo == "Lenda")
                {
                    if (Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno < Configuracoes.admJogo.jogadorAtual.maxLendasTurno) //pode baixar carta
                    {
                        //define o pai da carta para ser o grid lá do Cartas Baixadas
                        Configuracoes.BaixarCartaLenda(cartaAtual.valor.transform, gridArea.valor.transform, cartaAtual.valor);
                        cartaAtual.valor.logicaAtual = logicaCartaBaixa;
                        if (cartaAtual.valor.efeito != null)
                        {
                            cartaAtual.valor.efeito.cartaQueInvoca = cartaAtual.valor;
                        }
                        Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno++;
                        cartaAtual.valor.gameObject.SetActive(true);
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode baixar mais de uma Lenda por turno", Color.white);
                    }
                }
                if (cartaAtual.valor.infoCarta.carta.tipoCarta.nomeTipo == "Feitiço")
                {
                    if (Configuracoes.admJogo.jogadorAtual.podeUsarEfeito)
                    {
                        if (Configuracoes.admJogo.jogadorAtual.feiticosBaixadosNoTurno < Configuracoes.admJogo.jogadorAtual.maxFeiticosTurno)
                        {
                            Configuracoes.admJogo.efeitoAtual = cartaAtual.valor.efeito;
                            Configuracoes.RegistrarEvento("Escolha um alvo para o efeito de " + cartaAtual.valor.infoCarta.carta.name, Color.white);
                            Configuracoes.admJogo.DefinirEstado(usandoEfeito);
                        }
                        else
                        {
                            Configuracoes.RegistrarEvento("Você não pode baixar mais de um Feitiço por turno", Color.white);
                        }
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode utilizar efeitos neste turno", Color.white);
                    }

                }
            }
            // Dá um SetActive() pra sobrescrever o que tem no SelecaoAtual
            cartaAtual.valor.gameObject.SetActive(true);

        }

    }
}
