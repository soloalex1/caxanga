using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmJogo : MonoBehaviour
{
    public SeguradorDeJogador jogadorAtual;//variável que nos diz qual é o jogador atual.
    public SeguradorDeJogador jogadorLocal;
    public SeguradorDeJogador jogadorInimigo;
    public SeguradorDeJogador jogadorAtacado;
    [System.NonSerialized]
    public SeguradorDeJogador[] todosJogadores;
    public SeguradorDeCartas seguradorCartasJogadorPrincipal;
    public SeguradorDeCartas seguradorCartasJogadorQualquer;
    public EstadoJogador estadoAtual;//variávei que nos diz qual é o estado atual do jogador atual

    //definir no editor \/
    public GameObject prefabCarta;//quando formos instanciar uma carta, precisamos saber qual é a carta, por isso passamos essa referencia
    public int indiceTurno;
    public Turno[] turnos;
    public InfoUIJogador infoJogadorLocal;
    public InfoUIJogador infoJogadorInimigo;
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
        todosJogadores[1] = jogadorInimigo;
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
    void TrocarPosicaoJogadores()
    {
        if (jogadorAtual == jogadorLocal)
        {
            seguradorCartasJogadorPrincipal.CarregarCartasJogador(jogadorInimigo, infoJogadorLocal);
            seguradorCartasJogadorQualquer.CarregarCartasJogador(jogadorLocal, infoJogadorInimigo);

        }
        else
        {
            seguradorCartasJogadorPrincipal.CarregarCartasJogador(jogadorLocal, infoJogadorLocal);
            seguradorCartasJogadorQualquer.CarregarCartasJogador(jogadorInimigo, infoJogadorInimigo);
        }
    }
    void InicializarJogadores()
    {
        for (int i = 0; i < todosJogadores.Length; i++)
        {
            todosJogadores[i].magia = todosJogadores[i].magiaInicial;
            todosJogadores[i].vida = todosJogadores[i].vidaInicial;
            todosJogadores[i].barrasDeVida = 3;
            todosJogadores[i].baralho = new Baralho();
            todosJogadores[i].baralho.cartasBaralho = new List<string>();
            foreach (string carta in todosJogadores[i].baralhoInicial.cartasBaralho)
            {
                todosJogadores[i].baralho.cartasBaralho.Add(carta);
            }

            if (todosJogadores[i].jogadorHumano == true)
            {
                todosJogadores[i].seguradorCartasAtual = seguradorCartasJogadorPrincipal;

            }
            else
            {
                todosJogadores[i].seguradorCartasAtual = seguradorCartasJogadorQualquer;
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
        Configuracoes.DefinirPaiCarta(carta.transform, jogador.seguradorCartasAtual.gridMao.valor);//joga as cartas fisicamente na mão do jogador
        instCarta.podeSerAtacada = true;
        Configuracoes.RegistrarEvento("A carta " + instCarta + " foi puxada", jogador.corJogador);
        jogador.cartasMao.Add(instCarta);
        jogador.baralho.cartasBaralho.RemoveAt(jogador.baralho.cartasBaralho.Count - 1);
    }
    void PuxarCartasIniciais()
    {
        for (int p = 0; p < todosJogadores.Length; p++)
        {
            // todosJogadores[p].baralho.jogador = todosJogadores[p];
            for (int i = 0; i < todosJogadores[p].numCartasMaoInicio; i++)
            {
                PuxarCarta(todosJogadores[p]);
            }
            // for (int i = 0; i < todosJogadores[p].cartasMaoInicio.Length; i++)//para cada carta na mão do jogador atual...
            // {
            //     GameObject carta = Instantiate(prefabCarta) as GameObject;//instanciamos a carta de acordo com o prefab
            //     ExibirInfoCarta e = carta.GetComponent<ExibirInfoCarta>();//pegamos todas as informações atribuidas de texto e posição dela
            //     e.CarregarCarta(ar.obterInstanciaCarta(todosJogadores[p].cartasMaoInicio[i]));//e por fim dizemos que os textos escritos serão os da carta na mão do jogador
            //     InstanciaCarta instCarta = carta.GetComponent<InstanciaCarta>();
            //     instCarta.logicaAtual = todosJogadores[p].logicaMao;//define a lógica pra ser a lógica da mão
            //     Configuracoes.DefinirPaiCarta(carta.transform, todosJogadores[p].seguradorCartasAtual.gridMao.valor);//joga as cartas fisicamente na mão do jogador
            //     instCarta.podeSerAtacada = true;
            //     todosJogadores[p].cartasMao.Add(instCarta);
            // }
            Configuracoes.RegistrarEvento("Cartas do jogador(a) " + todosJogadores[p].nomeJogador + " foram criadas", todosJogadores[p].corJogador);
        }
    }

    public void TrocarJogadorAtual()
    {
        if (jogadorAtual == jogadorLocal)
        {
            jogadorAtual = jogadorInimigo;
        }
        else
        {
            jogadorAtual = jogadorLocal;
        }
    }
    private void Update()
    {
        bool foiCompleto = turnos[indiceTurno].Executar();
        Atacar();
        if (foiCompleto)
        {

            indiceTurno++;
            if (indiceTurno > turnos.Length - 1)
            {
                indiceTurno = 0;
            }
            //O jogador atual muda aqui
            TrocarPosicaoJogadores();
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

    public void FinalizarFaseAtual()
    {
        Configuracoes.RegistrarEvento(turnos[indiceTurno].name + " terminou", jogadorAtual.corJogador);
        turnos[indiceTurno].FinalizarFaseAtual();
    }
    public void Atacar()
    {
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
                Configuracoes.RegistrarEvento("O jogador " + jogadorAtual.nomeJogador + " derrotou o seu inimigo e venceu a rodada", jogadorAtual.corJogador);
                if (jogadorInimigo.barrasDeVida > 0)
                {
                    jogadorInimigo.barrasDeVida--;
                    if (jogadorInimigo.barrasDeVida <= 0)
                    {
                        Configuracoes.RegistrarEvento("O jogador " + jogadorAtual.nomeJogador + "venceu a partida", jogadorAtual.corJogador);
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
                jogador.cartasBaixadas.Remove(c);
            }
        }
    }
}
