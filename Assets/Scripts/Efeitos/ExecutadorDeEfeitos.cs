using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecutadorDeEfeitos : MonoBehaviour
{
    public GameEvent eventoAtivador;
    public EstadoJogador emSeuTurno;
    public EstadoJogador usandoEfeito;
    public Ativacao ativacaoAtiva, ativacaoReativa;
    public TipoEfeito tipoUnico, tipoPassivo;
    public ModoDeExecucao alterarMagiaJogador, alterarPoderCarta, alterarVidaJogador, alterarVidaOuPoder, cartaAtacaDuasVezes, paralisarCarta, protegerLenda, puxarCarta, reviverCarta, silenciarCarta, silenciarJogador;
    private void Start()
    {
        Configuracoes.admEfeito = this;
    }
    public void ExecutarEfeito()
    {

        if (eventoAtivador.cartaQueAtivouEvento != null && eventoAtivador.cartaQueAtivouEvento.efeito != null && eventoAtivador.cartaQueAtivouEvento.efeito.ativacao == ativacaoAtiva)
        {
            if (eventoAtivador == eventoAtivador.cartaQueAtivouEvento.efeito.eventoAtivador)
            {
                StartCoroutine(Efeito(eventoAtivador.cartaQueAtivouEvento.efeito));
            }
        }
    }
    public IEnumerator Efeito(Efeito efeito)
    {
        efeito.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
        if (efeito.escolheAlvoCarta == true)
        {
            Configuracoes.admJogo.efeitoAtual = eventoAtivador.cartaQueAtivouEvento.efeito;
            Configuracoes.admJogo.DefinirEstado(usandoEfeito);
            yield return new WaitWhile(() => efeito.cartaAlvo == null);
        }
        if (efeito.tipoEfeito == tipoUnico)
        {
            //efeito afeta apenas jogadores
            if (efeito.ativacao == ativacaoAtiva)
            {
                if (efeito.modoDeExecucao == alterarMagiaJogador)
                {
                    efeito.jogadorAlvo.magia += efeito.alteracaoMagia;
                    efeito.jogadorAlvo.CarregarInfoUIJogador();
                    efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                }
                if (efeito.modoDeExecucao == alterarVidaJogador)
                {
                    if (efeito.alteracaoVida > 0)
                        StartCoroutine(efeito.jogadorAlvo.infoUI.AnimacaoCura(efeito.alteracaoVida));
                    else
                        StartCoroutine(efeito.jogadorAlvo.infoUI.AnimacaoDano(efeito.alteracaoVida));

                    yield return new WaitForSeconds(0.8f);
                    efeito.jogadorAlvo.vida += efeito.alteracaoVida;
                    efeito.jogadorAlvo.CarregarInfoUIJogador();
                    efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                }
                if (efeito.modoDeExecucao == silenciarJogador)
                {
                    efeito.jogadorAlvo.podeUsarEfeito = false;
                }
                if (efeito.modoDeExecucao == alterarPoderCarta)
                {
                    if (efeito.afetaTodasCartas)
                    {
                        if (efeito.alteracaoPoder > 0)
                        {
                            foreach (InstanciaCarta c in efeito.jogadorQueInvoca.cartasBaixadas)
                            {
                                StartCoroutine(efeito.cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                                c.poder += efeito.alteracaoPoder;
                                c.infoCarta.CarregarCarta(efeito.cartaAlvo.infoCarta.carta);
                            }
                        }
                        else if (efeito.alteracaoPoder < 0)
                        {
                            foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasBaixadas)
                            {
                                if (c.podeSofrerEfeito)
                                {
                                    StartCoroutine(efeito.cartaAlvo.AnimacaoDano(efeito.alteracaoPoder));
                                    c.poder += efeito.alteracaoPoder;
                                    c.infoCarta.CarregarCarta(efeito.cartaAlvo.infoCarta.carta);
                                }
                            }
                        }
                        yield return new WaitForSeconds(0.8f);
                        efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                        if (efeito.cartaAlvo.poder <= 0)
                        {
                            Configuracoes.admJogo.MatarCarta(efeito.cartaAlvo, efeito.cartaAlvo.jogadorDono);
                        }
                    }
                    if (efeito.alteracaoPoder > 0)
                    {
                        StartCoroutine(efeito.cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                    }
                    else
                    {
                        StartCoroutine(efeito.cartaAlvo.AnimacaoDano(efeito.alteracaoPoder));
                    }
                    yield return new WaitForSeconds(0.8f);
                    efeito.cartaAlvo.poder += efeito.alteracaoPoder;
                    efeito.cartaAlvo.infoCarta.CarregarCarta(efeito.cartaAlvo.infoCarta.carta);
                    efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                    if (efeito.cartaAlvo.poder <= 0)
                    {
                        Configuracoes.admJogo.MatarCarta(efeito.cartaAlvo, efeito.cartaAlvo.jogadorDono);
                    }
                }
                if (efeito.modoDeExecucao == cartaAtacaDuasVezes)
                {
                    efeito.cartaAlvo.podeAtacarNesteTurno = true;
                }
                if (efeito.modoDeExecucao == paralisarCarta)
                {
                    if (efeito.cartaAlvo != null)
                        efeito.cartaAlvo.podeAtacarNesteTurno = false;
                }
                if (efeito.modoDeExecucao == protegerLenda)
                {
                    efeito.cartaAlvo.podeSofrerEfeito = false;
                }
                if (efeito.modoDeExecucao == silenciarCarta)
                {
                    efeito.cartaAlvo.podeAtacarNesteTurno = false;
                }
                if (efeito.modoDeExecucao == alterarVidaOuPoder)
                {
                    if (efeito.jogadorAlvo != null)
                    {
                        StartCoroutine(efeito.jogadorAlvo.infoUI.AnimacaoCura(efeito.alteracaoVida));
                        yield return new WaitForSeconds(0.8f);
                        efeito.jogadorAlvo.vida += efeito.alteracaoVida;
                        efeito.jogadorAlvo.CarregarInfoUIJogador();
                        efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                        yield return null;
                    }
                    if (efeito.cartaAlvo != null)
                    {
                        StartCoroutine(efeito.cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                        yield return new WaitForSeconds(0.8f);
                        efeito.cartaAlvo.poder += efeito.alteracaoPoder;
                        efeito.cartaAlvo.infoCarta.CarregarCarta(efeito.cartaAlvo.infoCarta.carta);
                        yield return null;
                    }
                }
                if (efeito.modoDeExecucao == puxarCarta)
                {
                    Configuracoes.admJogo.PuxarCarta(efeito.jogadorQueInvoca);
                    Configuracoes.admJogo.PuxarCarta(efeito.jogadorQueInvoca);
                }
                if (efeito.modoDeExecucao == reviverCarta)
                {
                    // Debug.Log("vou reviver do cemitério");
                }
                if (efeito.cartaQueInvoca.infoCarta.carta.tipoCarta.nomeTipo == "Feitiço")
                {
                    efeito.jogadorQueInvoca.CarregarInfoUIJogador();
                    Configuracoes.admJogo.DefinirEstado(emSeuTurno);
                    efeito.jogadorQueInvoca.ColocarCartaNoCemiterio(efeito.cartaQueInvoca);
                }
                efeito.cartaQueInvoca.efeitoUsado = true;
            }
            if (efeito.ativacao == ativacaoReativa)
            {
            }
        }
        if (efeito.tipoEfeito == tipoPassivo)
        {
        }
        Debug.Log("Ativei o efeito");
        yield return null;
    }
}
