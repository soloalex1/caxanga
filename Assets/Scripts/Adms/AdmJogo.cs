using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmJogo : MonoBehaviour
{
    bool fimDaRodada = false;
    public int rodadaAtual;
    public int numCartasPuxadasInicioRodada;
    public EstadoJogador EstadoInicialTurno;
    public SeguradorDeJogador jogadorAtual;//variável que nos diz qual é o jogador atual.
    public SeguradorDeJogador jogadorLocal;
    public SeguradorDeJogador jogadorInimigo;
    public SeguradorDeJogador jogadorAtacado;
    public SeguradorDeJogador jogadorIA;
    public Efeito efeitoAtual;

    [System.NonSerialized]
    public SeguradorDeJogador[] todosJogadores;
    public SeguradorDeCartas seguradorCartasJogadorAtual;
    public SeguradorDeCartas seguradorCartasJogadorInimigo;
    public EstadoJogador estadoAtual;//variávei que nos diz qual é o estado atual do jogador atual

    //definir no editor \/
    public GameObject prefabCarta;//quando formos instanciar uma carta, precisamos saber qual é a carta, por isso passamos essa referencia
    public int indiceTurno;
    public Turno[] turnos;
    public InfoUIJogador infoJogadorLocal;
    public InfoUIJogador infoJogadorIA;
    public InfoUIJogador[] infoJogadores;
    public VariavelString textoTurno;
    public GameEvent aoMudarTurno;
    public GameEvent aoMudarFase;
    public InstanciaCarta cartaAtacada;
    public InstanciaCarta cartaAtacante;

    public static AdmJogo singleton;
    private void Awake()
    {
        singleton = this;
        todosJogadores = new SeguradorDeJogador[turnos.Length];
        todosJogadores[0] = jogadorLocal;
        todosJogadores[1] = jogadorIA;
        jogadorAtual = jogadorLocal;
    }
    private void Start()
    {
        /*  
        A classe estática configurações vai possuir o admJogo como atributo,
        assim, nas configurações podemos mudar o admJogo também.
        */

        Configuracoes.admJogo = this;
        InicializarJogadores();
        PuxarCartasIniciais();
        textoTurno.valor = turnos[indiceTurno].jogador.nomeJogador;
        aoMudarTurno.Raise();
    }

    void InicializarJogadores()
    {
        for (int i = 0; i < todosJogadores.Length; i++)
        {
            todosJogadores[i].magia = todosJogadores[i].magiaInicial;
            todosJogadores[i].vida = todosJogadores[i].vidaInicial;
            todosJogadores[i].barrasDeVida = 3;
            todosJogadores[i].baralho = ScriptableObject.CreateInstance("Baralho") as Baralho;
            todosJogadores[i].baralho.cartasBaralho = new List<string>();
            todosJogadores[i].lendasBaixadasNoTurno = 0;
            todosJogadores[i].feiticosBaixadosNoTurno = 0;
            todosJogadores[i].podeUsarEfeito = true;
            todosJogadores[i].podeSerAtacado = true;

            foreach (string carta in todosJogadores[i].baralhoInicial.cartasBaralho)
            {
                todosJogadores[i].baralho.cartasBaralho.Add(carta);
            }

            if (todosJogadores[i].jogadorHumano == true)
            {
                todosJogadores[i].seguradorCartasAtual = seguradorCartasJogadorAtual;

            }
            else
            {
                todosJogadores[i].seguradorCartasAtual = seguradorCartasJogadorInimigo;
            }
            if (i < 2)
            {
                infoJogadores[i].jogador = todosJogadores[i];
                todosJogadores[i].infoUI = infoJogadores[i];
                infoJogadores[i].jogador.CarregarInfoUIJogador();
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
        Efeito novoEfeito = ScriptableObject.CreateInstance("Efeito") as Efeito;
        novoEfeito = e.carta.efeito;
        instCarta.efeito = novoEfeito;
        instCarta.efeito.cartaQueInvoca = instCarta;
        instCarta.efeito.jogadorQueInvoca = jogador;
        Configuracoes.DefinirPaiCarta(carta.transform, jogador.seguradorCartasAtual.gridMao.valor);//joga as cartas fisicamente na mão do jogador
        instCarta.podeSerAtacada = true;
        // Configuracoes.RegistrarEvento("A carta " + instCarta + " foi puxada", jogador.corJogador);
        jogador.cartasMao.Add(instCarta);
        jogador.baralho.cartasBaralho.RemoveAt(jogador.baralho.cartasBaralho.Count - 1);
    }
    void PuxarCartasIniciais()
    {
        for (int p = 0; p < todosJogadores.Length; p++)
        {
            todosJogadores[p].baralho.Embaralhar();
            for (int i = 0; i < todosJogadores[p].numCartasMaoInicio; i++)
            {
                PuxarCarta(todosJogadores[p]);
            }
            Configuracoes.RegistrarEvento("Cartas do jogador(a) " + todosJogadores[p].nomeJogador + " foram criadas", todosJogadores[p].corJogador);
        }
    }
    public void TrocarJogadorAtual()
    {
        //se na hora da troca o jogador de baixo for o Player
        if (jogadorAtual == jogadorLocal)
        {
            jogadorAtual = jogadorInimigo;
            jogadorInimigo = jogadorLocal;
            seguradorCartasJogadorAtual.CarregarCartasJogador(jogadorAtual, infoJogadorLocal);
            seguradorCartasJogadorInimigo.CarregarCartasJogador(jogadorInimigo, infoJogadorIA);
            seguradorCartasJogadorAtual.CarregarCemiterio(jogadorInimigo);
            seguradorCartasJogadorInimigo.CarregarCemiterio(jogadorAtual);
        }
        else
        {
            jogadorAtual = jogadorLocal;
            jogadorInimigo = jogadorIA;
            seguradorCartasJogadorAtual.CarregarCartasJogador(jogadorAtual, infoJogadorLocal);
            seguradorCartasJogadorInimigo.CarregarCartasJogador(jogadorInimigo, infoJogadorIA);
            seguradorCartasJogadorAtual.CarregarCemiterio(jogadorAtual);
            seguradorCartasJogadorInimigo.CarregarCemiterio(jogadorInimigo);
        }
    }
    private void Update()
    {
        bool foiCompleto = turnos[indiceTurno].Executar();
        Atacar();
        if (efeitoAtual != null)
        {
            DefinirEstado(efeitoAtual.usandoEfeito);
        }
        if (foiCompleto)
        {

            indiceTurno++;
            if (indiceTurno > turnos.Length - 1)
            {
                indiceTurno = 0;
            }
            //O jogador atual muda aqui
            TrocarJogadorAtual();
            turnos[indiceTurno].AoIniciarTurno();
            textoTurno.valor = turnos[indiceTurno].jogador.nomeJogador;
            aoMudarTurno.Raise();

        }

        if (estadoAtual != null)
        {
            estadoAtual.Tick(Time.deltaTime);//percorre as ações do jogador naquele estado e permite que ele as execute
        }
    }

    public void DefinirEstado(EstadoJogador estado)//função que altera o estado do jogador
    {
        estadoAtual = estado;
    }

    public void RedefinirJogadores()
    {
        if (fimDaRodada)
        {
            rodadaAtual++;
            //redefinir jogadores
            for (int i = 0; i < todosJogadores.Length; i++)
            {
                todosJogadores[i].magia = todosJogadores[i].magiaInicial + (2 * rodadaAtual);
                todosJogadores[i].vida = todosJogadores[i].vidaInicial;
                todosJogadores[i].lendasBaixadasNoTurno = 0;
                todosJogadores[i].feiticosBaixadosNoTurno = 0;
                todosJogadores[i].podeSerAtacado = true;
                todosJogadores[i].podeUsarEfeito = true;

                for (int j = 0; j < numCartasPuxadasInicioRodada; j++)
                {
                    PuxarCarta(todosJogadores[i]);
                }
                if (i < 2)
                {
                    infoJogadores[i].jogador = todosJogadores[i];
                    todosJogadores[i].infoUI = infoJogadores[i];
                    infoJogadores[i].jogador.CarregarInfoUIJogador();
                }

                foreach (InstanciaCarta carta in todosJogadores[i].cartasBaixadas)
                {
                    if (todosJogadores[i].cartasBaixadas.Contains(carta))
                    {
                        MatarCarta(carta, todosJogadores[i]);
                    }
                    if (todosJogadores[i].cartasBaixadas.Count <= 0)
                    {
                        break;
                    }
                }

            }
            // FinalizarFaseAtual();
            fimDaRodada = false;
            Configuracoes.RegistrarEvento("Mudando para a próxima rodada...", Color.white);
        }
    }
    public void FinalizarFaseAtual()
    {
        Configuracoes.RegistrarEvento(turnos[indiceTurno].name + " terminou", jogadorAtual.corJogador);
        turnos[indiceTurno].FinalizarFaseAtual();
    }
    public void Atacar()
    {
        //Atacar uma carta
        if (cartaAtacada != null && cartaAtacante != null)
        {
            int poderCartaAtacanteAntes = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            int poderCartaAtacadaAntes = cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor -= poderCartaAtacanteAntes;
            cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor -= poderCartaAtacadaAntes;
            Configuracoes.RegistrarEvento("A carta " + cartaAtacante.infoCarta.carta.name + " atacou a carta " + cartaAtacada.infoCarta.carta.name + " e tirou " + poderCartaAtacanteAntes, Color.white);
            int poderCartaAtacanteDepois = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            int poderCartaAtacadaDepois = cartaAtacada.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;

            if (poderCartaAtacadaDepois <= 0)
            {
                Configuracoes.RegistrarEvento(jogadorAtual.nomeJogador + " destruiu " + cartaAtacada.infoCarta.carta.name, jogadorAtual.corJogador);
                MatarCarta(cartaAtacada, jogadorInimigo);
            }
            if (poderCartaAtacanteDepois <= 0)
            {
                MatarCarta(cartaAtacante, jogadorAtual);
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
            int poderCartaAtacanteAntes = cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor;
            jogadorAtacado.vida -= poderCartaAtacanteAntes;
            cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor--;
            jogadorAtacado.infoUI.AtualizarVida();
            Configuracoes.RegistrarEvento(jogadorAtual.nomeJogador + " atacou " + jogadorInimigo.nomeJogador + " e lhe tirou " + poderCartaAtacanteAntes + " de vida", jogadorAtual.corJogador);
            cartaAtacante.infoCarta.CarregarCarta(cartaAtacante.infoCarta.carta);

            if (cartaAtacante.infoCarta.carta.AcharPropriedadePeloNome("Poder").intValor <= 0)
            {
                MatarCarta(cartaAtacante, jogadorAtual);
            }
            if (jogadorAtacado.vida <= 0)
            {
                if (jogadorInimigo.barrasDeVida > 0)
                {
                    Configuracoes.RegistrarEvento("O jogador " + jogadorAtual.nomeJogador + " derrotou o seu inimigo e venceu a rodada", jogadorAtual.corJogador);
                    fimDaRodada = true;
                    jogadorInimigo.barrasDeVida--;

                    if (jogadorInimigo.barrasDeVida <= 0)
                    {
                        Configuracoes.RegistrarEvento("O jogador " + jogadorAtual.nomeJogador + "venceu a partida", jogadorAtual.corJogador);
                        return;
                    }
                    else
                    {
                        FinalizarFaseAtual();
                        // TrocarJogadorAtual();
                        RedefinirJogadores();
                    }
                }
            }
            cartaAtacante.podeAtacarNesteTurno = false;
            cartaAtacante = null;
            jogadorAtacado = null;
        }
    }

    public void MatarCarta(InstanciaCarta c, SeguradorDeJogador jogador)
    {
        for (int i = 0; i < jogador.cartasBaixadas.Count; i++)
        {
            if (jogador.cartasBaixadas.Contains(c))
            {
                Configuracoes.RegistrarEvento(cartaAtacante.infoCarta.carta.name + " foi destruido(a) no combate", jogadorAtual.corJogador);
                c.gameObject.SetActive(false);
                jogador.ColocarCartaNoCemiterio(c);
                jogador.cartasBaixadas.Remove(c);
            }
        }
    }
}
