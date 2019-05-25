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
    public GameEvent jogadorAtivouEfeito;

    public override void Executar()
    {
        if (cartaAtual.valor != null)
        {

            InstanciaCarta c = cartaAtual.valor;

            bool temMagiaParaBaixarCarta = Configuracoes.admJogo.jogadorAtual.TemMagiaParaBaixarCarta(c);
            if (temMagiaParaBaixarCarta)
            {
                if (c.infoCarta.carta.tipoCarta.nomeTipo == "Lenda")
                {
                    if (Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno < Configuracoes.admJogo.jogadorAtual.maxLendasTurno) //pode baixar carta
                    {
                        //define o pai da carta para ser o grid lá do Cartas Baixadas
                        Configuracoes.admJogo.jogadorAtual.BaixarCarta(c.transform, gridArea.valor.transform, c);
                        c.logicaAtual = logicaCartaBaixa;
                        Configuracoes.admJogo.jogadorAtual.lendasBaixadasNoTurno++;
                        c.gameObject.SetActive(true);
                    }
                    else
                    {
                        Configuracoes.RegistrarEvento("Você não pode baixar mais de uma Lenda por turno", Color.white);
                    }
                }
                if (c.infoCarta.carta.tipoCarta.nomeTipo == "Feitiço")
                {
                    if (Configuracoes.admJogo.jogadorAtual.podeUsarEfeito)
                    {
                        if (Configuracoes.admJogo.jogadorAtual.feiticosBaixadosNoTurno < Configuracoes.admJogo.jogadorAtual.maxFeiticosTurno)
                        {
                            jogadorAtivouEfeito.cartaQueAtivouEvento = c;
                            Configuracoes.admEfeito.eventoAtivador = jogadorAtivouEfeito;
                            jogadorAtivouEfeito.Raise();
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
            c.gameObject.SetActive(true);

        }

    }
}
