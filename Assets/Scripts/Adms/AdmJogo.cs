using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AdmJogo : MonoBehaviour
{
    SeguradorDeJogador jogadorVencedor;
    public bool pause;
    public GameObject prefabCarta;//quando formos instanciar uma carta, precisamos saber qual é a carta, por isso passamos essa referencia

    public GameObject telaFimDeJogo;

    public GameObject telaPause;
    public int rodadaAtual;
    public EstadoJogador emSeuTurno;
    public Efeito efeitoAtual;
    public EstadoJogador estadoAtual;//variávei que nos diz qual é o estado atual do jogador atual
    public SeguradorDeJogador jogadorAtual;//variável que nos diz qual é o jogador atual.
    public SeguradorDeJogador jogadorLocal;
    public SeguradorDeJogador jogadorInimigo;
    public SeguradorDeJogador jogadorIA;

    public SeguradorDeCartas seguradorCartasJogadorLocal;
    public SeguradorDeCartas seguradorCartasJogadorInimigo;

    //definir no editor \/
    public InfoUIJogador infoJogadorLocal;
    public InfoUIJogador infoJogadorIA;
    public SeguradorDeJogador jogadorAtacado;
    public InstanciaCarta cartaAtacada;
    public InstanciaCarta cartaAtacante;
    public InstanciaCarta cartaAlvo;
    public SeguradorDeJogador jogadorAlvo;
    public Image ImagemTextoTurno;
    public Sprite cursorAlvoCinza, cursorSegurandoCarta, cursorIdle, cursorAlvoVermelho, cursorAlvoVerde;

    public static AdmJogo singleton;
    private void Awake()
    {
        singleton = this;
    }
    private void Start()
    {
        /*  
        A classe estática configurações vai possuir o admJogo como atributo,
        assim, nas configurações podemos mudar o admJogo também.
        */
        pause = false;
        Configuracoes.admJogo = this;
        jogadorLocal.InicializarJogador();
        jogadorIA.InicializarJogador();
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").SetActive(false);
        DefinirEstado(emSeuTurno);
        StartCoroutine(FadeTextoTurno(jogadorAtual));
    }

    private void Update()
    {
        if (!pause)
        {
            if (estadoAtual != null)
            {
                estadoAtual.Tick(Time.deltaTime);//percorre as ações do jogador naquele estado e permite que ele as execute
            }
        }

    }

    public void PuxarCarta(SeguradorDeJogador jogador)
    {
        AdmRecursos ar = Configuracoes.GetAdmRecursos();//precisamos acessar o admRecursos
        GameObject carta = Instantiate(prefabCarta) as GameObject;//instanciamos a carta de acordo com o prefab
        ExibirInfoCarta e = carta.GetComponent<ExibirInfoCarta>();//pegamos todas as informações atribuidas de texto e posição dela
        e.CarregarCarta(ar.obterInstanciaCarta(jogador.baralho.cartasBaralho[jogador.baralho.cartasBaralho.Count - 1]));//e por fim dizemos que os textos escritos serão os da carta na mão do jogador
        InstanciaCarta instCarta = carta.GetComponent<InstanciaCarta>();
        instCarta.logicaAtual = jogador.logicaMao;//define a lógica pra ser a lógica da mão
        e.instCarta = instCarta;
        if (e.carta.efeito != null)
        {
            Efeito novoEfeito = ScriptableObject.CreateInstance("Efeito") as Efeito;
            novoEfeito = e.carta.efeito;
            instCarta.efeito = novoEfeito;
            instCarta.efeito.cartaQueInvoca = instCarta;
            instCarta.efeito.jogadorQueInvoca = jogador;
        }
        instCarta.jogadorDono = jogador;
        Configuracoes.DefinirPaiCarta(carta.transform, jogador.seguradorCartas.gridMao.valor);//joga as cartas fisicamente na mão do jogador
        instCarta.podeSerAtacada = true;
        jogador.cartasMao.Add(instCarta);
        jogador.baralho.cartasBaralho.RemoveAt(jogador.baralho.cartasBaralho.Count - 1);
    }

    internal void DefinirEstado(object usadoEfeito)
    {
        throw new NotImplementedException();
    }

    IEnumerator FadeVencedorTurno(SeguradorDeJogador jogadorVencedorTurno)
    {
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Fundo turno/Turno").GetComponent<Text>().text = jogadorVencedorTurno.nomeJogador + "\nVenceu o Turno";
        ImagemTextoTurno.GetComponent<Image>().sprite = jogadorVencedorTurno.textoTurnoImage;
        ImagemTextoTurno.gameObject.SetActive(true);
        pause = true;
        yield return new WaitForSeconds(2);
        pause = false;
        ImagemTextoTurno.gameObject.SetActive(false);
        if (jogadorInimigo.barrasDeVida <= 0)
        {
            jogadorVencedor = jogadorAtual;
            StartCoroutine(FimDeJogo(jogadorVencedor));
            yield return null;
        }
        if (jogadorAtual.barrasDeVida <= 0)
        {
            jogadorVencedor = jogadorInimigo;
            StartCoroutine(FimDeJogo(jogadorVencedor));
            yield return null;
        }
        TrocarJogadorAtual();
        jogadorAtual.rodada.IniciarRodada();
        jogadorInimigo.rodada.IniciarRodada();

    }
    void ChecaVidaJogadores()
    {
        if (jogadorAtual.vida <= 0 || jogadorInimigo.vida <= 0)
        {
            if (jogadorAtual.vida <= 0)
            {
                jogadorAtual.barrasDeVida--;
                StartCoroutine(FadeVencedorTurno(jogadorInimigo));
            }
            else if (jogadorInimigo.vida <= 0)
            {
                jogadorInimigo.barrasDeVida--;
                StartCoroutine(FadeVencedorTurno(jogadorAtual));
            }
            else
            {
                Debug.Log("EMPATE");
                jogadorAtual.barrasDeVida--;
                jogadorInimigo.barrasDeVida--;
                jogadorAtual.rodada.PassarRodada();
                jogadorInimigo.rodada.IniciarRodada();
            }
            // yield return new WaitWhile(() => mostrouVencedorTurno == false);
            // if (jogadorInimigo.barrasDeVida <= 0)
            // {
            //     jogadorVencedor = jogadorAtual;
            //     StartCoroutine(FimDeJogo(jogadorVencedor));
            // }
            // if (jogadorAtual.barrasDeVida <= 0)
            // {
            //     jogadorVencedor = jogadorInimigo;
            //     StartCoroutine(FimDeJogo(jogadorVencedor));
            // }
        }
    }
    public void TrocarJogadorAtual()
    {
        //se na hora da troca o jogador de baixo for o Player
        if (jogadorAtual == jogadorLocal)
        {
            jogadorAtual = jogadorInimigo;
            jogadorInimigo = jogadorLocal;
        }
        else
        {
            jogadorAtual = jogadorLocal;
            jogadorInimigo = jogadorIA;
        }
        seguradorCartasJogadorLocal.CarregarCartasJogador(jogadorAtual, infoJogadorLocal);
        seguradorCartasJogadorInimigo.CarregarCartasJogador(jogadorInimigo, infoJogadorIA);

        jogadorAtual.rodada.turno.IniciarTurno();
    }

    public void Pausar()
    {
        Configuracoes.admCursor.MudarSprite(cursorIdle);
        telaPause.gameObject.SetActive(true);
        pause = true;
    }
    public void Retomar()
    {
        if (estadoAtual == emSeuTurno)
        {
            Configuracoes.admCursor.MudarSprite(cursorIdle);
        }
        if (estadoAtual.name == "Atacando" || estadoAtual.name == "Usando Efeito")
        {
            Configuracoes.admCursor.MudarSprite(cursorAlvoCinza);
        }
        pause = false;
        telaPause.gameObject.SetActive(false);
    }

    public IEnumerator FimDeJogo(SeguradorDeJogador jogadorVencedor)
    {
        telaFimDeJogo.gameObject.SetActive(true);
        telaFimDeJogo.transform.Find("Retrato Jogador").GetComponent<Image>().sprite = jogadorVencedor.retratoJogador;
        telaFimDeJogo.transform.Find("Moldura").GetComponent<Image>().sprite = jogadorVencedor.moldura;
        yield return new WaitForSeconds(2);
        telaFimDeJogo.gameObject.SetActive(false);
        Pausar();
    }
    public void DefinirEstado(EstadoJogador estado)//função que altera o estado do jogador
    {
        estadoAtual = estado;
        Sprite spriteCursor = null;
        if (estado.name == "Em Seu Turno")
        {
            spriteCursor = cursorIdle;
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Cursor").GetComponent<RectTransform>().pivot = new Vector2(0, 1);
        }
        if (estado.name == "Atacando" || estado.name == "Usando Efeito")
        {
            if (estado.name == "Atacando")
            {
                spriteCursor = cursorAlvoVermelho;
            }
            spriteCursor = cursorAlvoCinza;
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Cursor").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }
        if (estado.name == "Segurando Carta")
        {
            spriteCursor = cursorSegurandoCarta;
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Cursor").GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }
        if (Configuracoes.admCursor != null)
            Configuracoes.admCursor.MudarSprite(spriteCursor);
    }


    public IEnumerator FadeTextoTurno(SeguradorDeJogador jogador)
    {
        if (jogador.passouRodada == false)
        {
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Fundo turno/Turno").GetComponent<Text>().text = "Turno de\n" + jogador.nomeJogador;
            ImagemTextoTurno.GetComponent<Image>().sprite = jogadorAtual.textoTurnoImage;
            ImagemTextoTurno.gameObject.SetActive(true);
            pause = true;
            yield return new WaitForSeconds(1);
            pause = false;
            ImagemTextoTurno.gameObject.SetActive(false);
        }
    }
    public void EncerrarRodada()
    {
        jogadorAtual.rodada.turno.FinalizarTurno();
        if (jogadorAtual.fezAlgumaAcao)//somente passou o turno
        {
            if (jogadorInimigo.passouRodada == false)
            {
                TrocarJogadorAtual();
                jogadorAtual.rodada.turno.IniciarTurno();
            }
            else
            {
                ChecaVidaJogadores();
                jogadorAtual.rodada.turno.IniciarTurno();
            }
        }
        else//pretende encerrar a rodada
        {
            jogadorAtual.rodada.PassarRodada();
            if (jogadorInimigo.passouRodada && jogadorAtual.passouRodada)
            {
                if (jogadorAtual.vida > jogadorInimigo.vida)
                {
                    jogadorInimigo.barrasDeVida--;
                    StartCoroutine(FadeVencedorTurno(jogadorAtual));
                }
                if (jogadorAtual.vida < jogadorInimigo.vida)
                {
                    jogadorAtual.barrasDeVida--;
                    StartCoroutine(FadeVencedorTurno(jogadorInimigo));
                }
            }
            else
            {
                TrocarJogadorAtual();
                jogadorAtual.rodada.turno.IniciarTurno();
            }
        }
    }
    public IEnumerator Atacar()
    {
        //Atacar uma carta
        if (cartaAtacada != null && cartaAtacante != null)
        {
            cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
            int poderCartaAtacanteAntes = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            int poderCartaAtacadaAntes = cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            StartCoroutine(cartaAtacada.AnimacaoDano(poderCartaAtacanteAntes * -1));
            StartCoroutine(cartaAtacante.AnimacaoDano(poderCartaAtacadaAntes * -1));
            yield return new WaitForSeconds(0.8f);
            cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor -= poderCartaAtacanteAntes;
            cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor -= poderCartaAtacadaAntes;
            Configuracoes.RegistrarEvento("A carta " + cartaAtacante.infoCarta.carta.name + " atacou a carta " + cartaAtacada.infoCarta.carta.name + " e tirou " + poderCartaAtacanteAntes, Color.white);
            int poderCartaAtacanteDepois = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            int poderCartaAtacadaDepois = cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;

            if (poderCartaAtacadaDepois <= 0)
            {
                Configuracoes.RegistrarEvento(jogadorAtual.nomeJogador + " destruiu " + cartaAtacada.infoCarta.carta.name, jogadorAtual.corJogador);

                if (cartaAtacada.efeito != null && cartaAtacada.efeito.eventoAtivador.name == "Carta Morreu")
                {
                    StartCoroutine("ExecutarEfeito", cartaAtacada.efeito);
                }
                MatarCarta(cartaAtacada, cartaAtacada.jogadorDono);
            }
            if (poderCartaAtacanteDepois <= 0)
            {
                MatarCarta(cartaAtacante, cartaAtacante.jogadorDono);
            }
            cartaAtacante.infoCarta.CarregarCarta(cartaAtacante.infoCarta.carta);
            cartaAtacada.infoCarta.CarregarCarta(cartaAtacada.infoCarta.carta);
            cartaAtacante.podeAtacarNesteTurno = false;
            cartaAtacada = null;
            cartaAtacante = null;
        }
        //Atacar um jogador
        if (cartaAtacante != null && jogadorAtacado != null)
        {
            cartaAtacante.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
            int poderCartaAtacanteAntes = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            StartCoroutine(jogadorAtacado.infoUI.AnimacaoDano(poderCartaAtacanteAntes));
            StartCoroutine(cartaAtacante.AnimacaoDano(-1));
            yield return new WaitForSeconds(0.8f);
            jogadorAtacado.vida -= poderCartaAtacanteAntes;
            cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor--;
            jogadorAtacado.infoUI.AtualizarVida();
            Configuracoes.RegistrarEvento(jogadorAtual.nomeJogador + " atacou " + jogadorInimigo.nomeJogador + " e lhe tirou " + poderCartaAtacanteAntes + " de vida", jogadorAtual.corJogador);
            cartaAtacante.infoCarta.CarregarCarta(cartaAtacante.infoCarta.carta);
            if (cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor <= 0)
            {
                MatarCarta(cartaAtacante, cartaAtacante.jogadorDono);
            }
            cartaAtacante.podeAtacarNesteTurno = false;
            cartaAtacante = null;
            jogadorAtacado = null;
            ChecaVidaJogadores();
        }
        yield return null;
    }
    public void MatarCarta(InstanciaCarta c, SeguradorDeJogador jogador)
    {
        for (int i = 0; i < jogador.cartasBaixadas.Count; i++)
        {
            if (jogador.cartasBaixadas.Contains(c))
            {
                c.gameObject.SetActive(false);
                jogador.ColocarCartaNoCemiterio(c);
                jogador.cartasBaixadas.Remove(c);
            }
        }
    }
    public IEnumerator ExecutarEfeito(Efeito efeito)
    {
        efeito.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
        if (efeito.tipoEfeito.tipoNome == "Passivo")
        {
        }
        else if (efeito.tipoEfeito.tipoNome == "Único")
        {
            //efeito afeta apenas jogadores
            if (efeito.modoDeExecucao.nomeModo == "Alterar Magia Jogador"
                || efeito.modoDeExecucao.nomeModo == "Alterar Vida Jogador"
                || efeito.modoDeExecucao.nomeModo == "Silenciar Jogador")
            {
                if (efeito.ativacao.nomeAtivacao == "Ativa")
                {
                    if (efeito.cartaQueInvoca.infoCarta.carta.tipoCarta.nomeTipo == "Feitiço")
                    {
                        jogadorAtual.CarregarInfoUIJogador();
                        DefinirEstado(emSeuTurno);
                        jogadorAtual.ColocarCartaNoCemiterio(efeito.cartaQueInvoca);
                    }
                }
                if (efeito.modoDeExecucao.nomeModo == "Alterar Magia Jogador")
                {
                    jogadorAlvo.magia += efeito.alteracaoMagia;
                    jogadorAlvo.CarregarInfoUIJogador();
                    jogadorAtual.CarregarInfoUIJogador();
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " alterou magia de " + jogadorAlvo.nomeJogador + " em " + efeito.alteracaoVida, Color.white);
                }
                else if (efeito.modoDeExecucao.nomeModo == "Alterar Vida Jogador")
                {
                    if (efeito.alteracaoVida > 0)
                        StartCoroutine(jogadorAlvo.infoUI.AnimacaoCura(efeito.alteracaoVida));
                    else
                        StartCoroutine(jogadorAlvo.infoUI.AnimacaoDano(efeito.alteracaoVida));

                    yield return new WaitForSeconds(0.8f);
                    jogadorAlvo.vida += efeito.alteracaoVida;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " alterou vida de " + jogadorAlvo.nomeJogador + " em " + efeito.alteracaoVida, Color.white);
                    jogadorAlvo.CarregarInfoUIJogador();
                    jogadorAtual.CarregarInfoUIJogador();
                }
                else if (efeito.modoDeExecucao.nomeModo == "Silenciar Jogador")
                {
                    jogadorAlvo.podeUsarEfeito = false;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " silenciou " + jogadorAlvo.nomeJogador, Color.white);
                }
            }
            if (efeito.modoDeExecucao.nomeModo == "Alterar Poder Carta"
                || efeito.modoDeExecucao.nomeModo == "Carta Ataca Duas Vezes"
                || efeito.modoDeExecucao.nomeModo == "Paralisar Carta"
                || efeito.modoDeExecucao.nomeModo == "Proteger Lenda"
                || efeito.modoDeExecucao.nomeModo == "Silenciar Carta")
            {
                if (efeito.modoDeExecucao.nomeModo == "Alterar Poder Carta")
                {
                    if (efeito.afetaTodasCartas)
                    {
                        if (efeito.alteracaoPoder > 0)
                        {
                            foreach (InstanciaCarta c in jogadorAtual.cartasBaixadas)
                            {
                                StartCoroutine(cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                                c.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += efeito.alteracaoPoder;
                                c.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                            }
                        }
                        else if (efeito.alteracaoPoder < 0)
                        {
                            foreach (InstanciaCarta c in jogadorInimigo.cartasBaixadas)
                            {
                                if (c.podeSofrerEfeito)
                                {
                                    StartCoroutine(cartaAlvo.AnimacaoDano(efeito.alteracaoPoder));
                                    c.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += efeito.alteracaoPoder;
                                    c.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                                }
                            }
                        }
                        yield return new WaitForSeconds(0.8f);
                        jogadorAtual.CarregarInfoUIJogador();
                        if (cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor <= 0)
                        {
                            MatarCarta(cartaAlvo, cartaAlvo.jogadorDono);
                        }
                    }
                    if (efeito.alteracaoPoder > 0)
                    {
                        StartCoroutine(cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                    }
                    else
                    {
                        StartCoroutine(cartaAlvo.AnimacaoDano(efeito.alteracaoPoder));
                    }
                    yield return new WaitForSeconds(0.8f);
                    cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += efeito.alteracaoPoder;
                    cartaAlvo.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " alterou o poder de " + cartaAlvo.infoCarta.carta.name + " em " + efeito.alteracaoPoder, Color.white);
                    jogadorAtual.CarregarInfoUIJogador();
                    if (cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor <= 0)
                    {
                        MatarCarta(cartaAlvo, cartaAlvo.jogadorDono);
                    }
                }
                if (efeito.modoDeExecucao.nomeModo == "Carta Ataca Duas Vezes")
                {
                    cartaAlvo.podeAtacarNesteTurno = true;
                }
                if (efeito.modoDeExecucao.nomeModo == "Paralisar Carta")
                {
                    if (cartaAlvo != null)
                        cartaAlvo.podeAtacarNesteTurno = false;
                }
                if (efeito.modoDeExecucao.nomeModo == "Proteger Lenda")
                {
                    cartaAlvo.podeSofrerEfeito = false;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.name + " silenciou " + cartaAlvo.infoCarta.carta.name, Color.white);
                }
                if (efeito.modoDeExecucao.nomeModo == "Silenciar Carta")
                {
                    cartaAlvo.podeAtacarNesteTurno = false;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.name + " silenciou" + cartaAlvo.infoCarta.carta.name, Color.white);
                }
            }
            if (efeito.modoDeExecucao.nomeModo == "Alterar Vida Jogador ou Alterar Poder Carta")
            {
                if (jogadorAlvo != null)
                {
                    StartCoroutine(jogadorAlvo.infoUI.AnimacaoCura(efeito.alteracaoVida));
                    yield return new WaitForSeconds(0.8f);
                    jogadorAlvo.vida += efeito.alteracaoVida;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " alterou vida de " + jogadorAlvo.nomeJogador + " em " + efeito.alteracaoVida, Color.white);
                    jogadorAlvo.CarregarInfoUIJogador();
                    jogadorAtual.CarregarInfoUIJogador();
                    yield return null;
                }
                if (cartaAlvo != null)
                {
                    StartCoroutine(cartaAlvo.AnimacaoCura(efeito.alteracaoPoder));
                    yield return new WaitForSeconds(0.8f);
                    cartaAlvo.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor += efeito.alteracaoPoder;
                    cartaAlvo.infoCarta.CarregarCarta(cartaAlvo.infoCarta.carta);
                    yield return null;
                    Configuracoes.RegistrarEvento(efeito.cartaQueInvoca.infoCarta.carta.name + " alterou o poder de " + cartaAlvo.infoCarta.carta.name + " em " + efeito.alteracaoPoder, Color.white);
                }
            }
            if (efeito.modoDeExecucao.nomeModo == "Puxar Carta")
            {
                PuxarCarta(jogadorAtual);
                PuxarCarta(jogadorAtual);
            }
            if (efeito.modoDeExecucao.nomeModo == "Reviver do Cemitério")
            {
                // Debug.Log("vou reviver do cemitério");
            }
            efeito.cartaQueInvoca.efeitoUsado = true;
        }
        yield return null;
    }
}