using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AdmJogo : MonoBehaviour
{
    public Baralho baralhoTutorial1, baralhoTutorial2;
    public bool tutorial, inicioTutorial;
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
    public GameEvent cartaMatou;
    public Image ImagemTextoTurno;
    public Sprite cursorAlvoCinza, cursorSegurandoCarta, cursorIdle, cursorAlvoVermelho, cursorAlvoVerde;
    public VariavelCarta cartaAtual;
    public GameEvent aoOlharCarta, aoPararDeOlharCarta;

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
        if (inicioTutorial)
        {
            jogadorLocal.baralho = baralhoTutorial1;
            jogadorInimigo.baralho = baralhoTutorial2;
            pause = true;
        }
        else
        {
            pause = false;
        }

        Configuracoes.admJogo = this;
        jogadorLocal.InicializarJogador();
        jogadorIA.InicializarJogador();
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Texto Passou").SetActive(false);
        DefinirEstado(emSeuTurno);
        if (tutorial == false)
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
        InstanciaCarta instCarta = carta.GetComponent<InstanciaCarta>();
        e.CarregarCarta(ar.obterInstanciaCarta(jogador.baralho.cartasBaralho[jogador.baralho.cartasBaralho.Count - 1]));//e por fim dizemos que os textos escritos serão os da carta na mão do jogador
        instCarta.carta = e.carta;
        instCarta.SetPoderECusto();
        e.CarregarCarta(instCarta.carta);
        instCarta.logicaAtual = jogador.logicaMao;//define a lógica pra ser a lógica da mão
        if (instCarta.carta.efeito != null)
        {
            Efeito novoEfeito = ScriptableObject.CreateInstance("Efeito") as Efeito;
            // novoEfeito = instCarta.carta.efeito;
            novoEfeito.name = instCarta.carta.efeito.name;
            novoEfeito.afetaApenasSeuJogador = instCarta.carta.efeito.afetaApenasSeuJogador;
            novoEfeito.afetaTodasCartas = instCarta.carta.efeito.afetaTodasCartas;
            novoEfeito.alteracaoMagia = instCarta.carta.efeito.alteracaoMagia;
            novoEfeito.alteracaoPoder = instCarta.carta.efeito.alteracaoPoder;
            novoEfeito.alteracaoVida = instCarta.carta.efeito.alteracaoVida;
            novoEfeito.apenasJogador = instCarta.carta.efeito.apenasJogador;
            novoEfeito.ativacao = instCarta.carta.efeito.ativacao;
            novoEfeito.cartaAlvo = instCarta.carta.efeito.cartaAlvo;
            novoEfeito.cartaQueInvoca = instCarta;
            novoEfeito.condicaoAtivacao = instCarta.carta.efeito.condicaoAtivacao;
            novoEfeito.escolheAlvoCarta = instCarta.carta.efeito.escolheAlvoCarta;
            novoEfeito.eventoAtivador = instCarta.carta.efeito.eventoAtivador;
            novoEfeito.jogadorAlvo = instCarta.carta.efeito.jogadorAlvo;
            novoEfeito.jogadorQueInvoca = jogador;
            novoEfeito.modoDeExecucao = instCarta.carta.efeito.modoDeExecucao;
            novoEfeito.podeUsarEmSi = instCarta.carta.efeito.podeUsarEmSi;
            novoEfeito.tipoEfeito = instCarta.carta.efeito.tipoEfeito;
            instCarta.efeito = novoEfeito;

            if (instCarta.efeito.apenasJogador)
            {
                //afeta vc
                if (instCarta.efeito.afetaApenasSeuJogador)
                {
                    instCarta.efeito.jogadorAlvo = jogador;
                }
                else//afeta o inimigo
                {
                    if (jogador == jogadorIA)
                    {
                        instCarta.efeito.jogadorAlvo = jogadorLocal;
                    }
                    else
                    {
                        instCarta.efeito.jogadorAlvo = jogadorIA;
                    }
                }
            }
        }
        instCarta.jogadorDono = jogador;
        Configuracoes.DefinirPaiCarta(carta.transform, jogador.seguradorCartas.gridMao.valor);//joga as cartas fisicamente na mão do jogador
        jogador.cartasMao.Add(instCarta);
        jogador.baralho.cartasBaralho.RemoveAt(jogador.baralho.cartasBaralho.Count - 1);
    }
    IEnumerator FadeVencedorTurno(SeguradorDeJogador jogadorVencedorTurno)
    {
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Fundo turno/Turno").GetComponent<Text>().color = jogadorVencedor.corJogador;
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Fundo turno/Turno").GetComponent<Text>().text = jogadorVencedorTurno.nomeJogador + "\nVenceu a Rodada";
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
        jogadorAtual.rodada.IniciarRodada();
        jogadorInimigo.rodada.IniciarRodada();
        TrocarJogadorAtual();


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
            int poderCartaAtacanteAntes = cartaAtacante.poder;
            int poderCartaAtacadaAntes = cartaAtacada.poder;
            StartCoroutine(cartaAtacada.AnimacaoDano(poderCartaAtacanteAntes * -1));
            StartCoroutine(cartaAtacante.AnimacaoDano(poderCartaAtacadaAntes * -1));
            yield return new WaitForSeconds(0.8f);
            cartaAtacada.poder -= poderCartaAtacanteAntes;
            cartaAtacante.poder -= poderCartaAtacadaAntes;
            Configuracoes.RegistrarEvento("A carta " + cartaAtacante.infoCarta.carta.name + " atacou a carta " + cartaAtacada.infoCarta.carta.name + " e tirou " + poderCartaAtacanteAntes, Color.white);
            int poderCartaAtacanteDepois = cartaAtacante.poder;
            int poderCartaAtacadaDepois = cartaAtacada.poder;

            if (poderCartaAtacadaDepois <= 0)
            {

                cartaMatou.cartaQueAtivouEvento = cartaAtacante;
                Configuracoes.admEfeito.eventoAtivador = cartaMatou;
                cartaMatou.Raise();
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
            int poderCartaAtacanteAntes = cartaAtacante.poder;
            StartCoroutine(jogadorAtacado.infoUI.AnimacaoDano(poderCartaAtacanteAntes * -1));
            StartCoroutine(cartaAtacante.AnimacaoDano(-1));
            yield return new WaitForSeconds(0.8f);
            jogadorAtacado.vida -= poderCartaAtacanteAntes;
            cartaAtacante.poder--;
            jogadorAtacado.infoUI.AtualizarVida();
            Configuracoes.RegistrarEvento(jogadorAtual.nomeJogador + " atacou " + jogadorInimigo.nomeJogador + " e lhe tirou " + poderCartaAtacanteAntes + " de vida", jogadorAtual.corJogador);
            cartaAtacante.infoCarta.CarregarCarta(cartaAtacante.infoCarta.carta);
            if (cartaAtacante.poder <= 0)
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
        if (jogador.cartasBaixadas.Count == 0)
        {
            return;
        }
        foreach (InstanciaCarta carta in jogador.cartasBaixadas)
        {
            if (jogador.cartasBaixadas.Contains(c))
            {
                c.gameObject.SetActive(false);
                jogador.ColocarCartaNoCemiterio(c);
                jogador.cartasBaixadas.Remove(c);
                break;
            }
        }
    }

    public IEnumerator DestacarCartaBaixada(InstanciaCarta instCarta){
        // fechando outras cartas em destaque   
        aoPararDeOlharCarta.Raise();

        cartaAtual.Set(instCarta);

        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Carta Sendo Olhada/Carta sendo olhada").SetActive(true);
        Configuracoes.cartaRecemJogada = true;
        aoOlharCarta.Raise();

        yield return new WaitForSeconds(1.5f);

        Configuracoes.cartaRecemJogada = false;

        aoPararDeOlharCarta.Raise();
    }
}