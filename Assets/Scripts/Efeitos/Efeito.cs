using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeito")]
public class Efeito : ScriptableObject
{
    public SeguradorDeJogador jogadorQueInvoca;
    public SeguradorDeJogador jogadorAlvo;
    public InstanciaCarta cartaQueInvoca;
    public InstanciaCarta cartaAlvo;
    public string modoEfeito;
    public int alteracaoPoder;
    public int alteracaoVida;
    public bool apenasJogador;
    public bool apenasCarta;
    // Dictionary<string, Efeito> dicionarioEfeitos = new Dictionary<string, Efeito>();
    public EstadoJogador usandoEfeito;
    public bool podeUsarEmSi;
    public void ExecutarEfeito()
    {
        if (cartaQueInvoca.efeitoUsado == false)
        {
            if (modoEfeito == "AlterarPoderCarta")
            {
                if (Configuracoes.admJogo.efeitoAtual.cartaAlvo != null)
                {
                    cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += alteracaoPoder;
                    cartaAlvo.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                    Configuracoes.RegistrarEvento(cartaQueInvoca.infoCarta.carta.name + " alterou o poder de " + cartaAlvo.infoCarta.carta.name + " em " + alteracaoPoder, Color.white);
                }
            }
            if (modoEfeito == "AlterarVidaJogador")
            {
                if (apenasJogador)
                {
                    if (Configuracoes.admJogo.efeitoAtual.cartaAlvo != null)
                    {
                        Configuracoes.RegistrarEvento("O efeito desta carta só se aplica a jogadores", Color.white);
                        Configuracoes.admJogo.efeitoAtual = null;
                        return;
                    }
                }
                if (Configuracoes.admJogo.efeitoAtual.jogadorAlvo != null)
                {
                    Configuracoes.admJogo.efeitoAtual.jogadorAlvo.vida += alteracaoVida;
                    Configuracoes.admJogo.efeitoAtual.jogadorAlvo.CarregarInfoUIJogador();
                    Configuracoes.RegistrarEvento(cartaQueInvoca.infoCarta.carta.name + " alterou a vida de " + Configuracoes.admJogo.efeitoAtual.jogadorAlvo.nomeJogador + " em " + alteracaoVida, Color.white);
                }
            }
            if (modoEfeito == "AlterarPoderCarta e AlterarVidaJogador")
            {
                if (Configuracoes.admJogo.efeitoAtual.cartaAlvo != null)
                {
                    cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += alteracaoPoder;
                    cartaAlvo.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                    Configuracoes.RegistrarEvento(cartaQueInvoca.infoCarta.carta.name + " alterou o poder de " + cartaAlvo.infoCarta.carta.name + " em " + alteracaoPoder, Color.white);
                    Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.efeitoUsado = true;
                    Configuracoes.admJogo.efeitoAtual = null;
                    return;
                }
                if (Configuracoes.admJogo.efeitoAtual.jogadorAlvo != null)
                {
                    Configuracoes.admJogo.efeitoAtual.jogadorAlvo.vida += alteracaoPoder;
                    Configuracoes.admJogo.efeitoAtual.jogadorAlvo.CarregarInfoUIJogador();
                    Configuracoes.RegistrarEvento(cartaQueInvoca.infoCarta.carta.name + " alterou a vida de " + Configuracoes.admJogo.efeitoAtual.jogadorAlvo.nomeJogador + " em " + alteracaoPoder, Color.white);
                    Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.efeitoUsado = true;
                    Configuracoes.admJogo.efeitoAtual = null;
                    return;
                }
            }
            if (Configuracoes.admJogo.jogadorAtual.cartasMao.Contains(cartaQueInvoca))
            {
                Configuracoes.admJogo.jogadorAtual.cartasMao.Remove(cartaQueInvoca);
                cartaQueInvoca.gameObject.SetActive(false);
                jogadorQueInvoca.ColocarCartaNoCemiterio(cartaQueInvoca);
                Configuracoes.admJogo.jogadorAtual.feiticosBaixadosNoTurno++;
                Configuracoes.RegistrarEvento(cartaQueInvoca.infoCarta.carta.name + " foi para o cemitério após o uso", Color.white);
            }
            Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.efeitoUsado = true;
            Configuracoes.admJogo.efeitoAtual = null;
        }
    }
}
