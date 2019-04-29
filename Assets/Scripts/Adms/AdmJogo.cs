using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmJogo : MonoBehaviour
{
    public SeguradorDeJogador jogadorAtual;//variável que nos diz qual é o jogador atual.
    public SeguradorDeJogador[] todosJogadores;
    public SeguradorDeCartas seguradorJogadorPrincipal;
    public SeguradorDeCartas seguradorJogadorQualquer;

    public EstadoJogador estadoAtual;//variávei que nos diz qual é o estado atual do jogador atual

    //definir no editor \/
    public GameObject prefabCarta;//quando formos instanciar uma carta, precisamos saber qual é a carta, por isso passamos essa referencia
    public int indiceTurno;
    public Turno[] turnos;
    public VariavelString textoTurno;
    public GameEvent aoMudarTurno;
    public GameEvent aoMudarFase;


    private void Start()
    {
        /*  
        A classe estática configurações vai possuir o admJogo como atributo,
        assim, nas configurações podemos mudar o admJogo também.
        */
        Configuracoes.admJogo = this;

        InicializarJogadores();

        CriarCartasIniciais();
        textoTurno.valor = turnos[indiceTurno].jogador.nomeJogador;
        aoMudarTurno.Raise();
    }

    void InicializarJogadores()
    {
        foreach (SeguradorDeJogador jogador in todosJogadores)
        {
            jogador.magia = 10;
            jogador.vida = 20;
            if (jogador.jogadorHumano == true)
            {
                jogador.seguradorAtual = seguradorJogadorPrincipal;
            }
            else
            {
                jogador.seguradorAtual = seguradorJogadorQualquer;
            }
        }
    }

    void CriarCartasIniciais()
    {
        AdmRecursos ar = Configuracoes.GetAdmRecursos();//precisamos acessar o admRecursos

        for (int p = 0; p < todosJogadores.Length; p++)
        {
            for (int i = 0; i < todosJogadores[p].cartasMaoInicio.Length; i++)//para cada carta na mão do jogador atual...
            {
                GameObject carta = Instantiate(prefabCarta) as GameObject;//instanciamos a carta de acordo com o prefab
                ExibirInfoCarta e = carta.GetComponent<ExibirInfoCarta>();//pegamos todas as informações atribuidas de texto e posição dela
                e.CarregarCarta(ar.obterInstanciaCarta(todosJogadores[p].cartasMaoInicio[i]));//e por fim dizemos que os textos escritos serão os da carta na mão do jogador
                InstanciaCarta instCarta = carta.GetComponent<InstanciaCarta>();
                instCarta.logicaAtual = todosJogadores[p].logicaMao;//define a lógica pra ser a lógica da mão
                Configuracoes.DefinirPaiCarta(carta.transform, todosJogadores[p].seguradorAtual.gridMao.valor);//joga as cartas fisicamente na mão do jogador
                todosJogadores[p].cartasMao.Add(instCarta);
            }
            Configuracoes.RegistrarEvento("Cartas do jogador(a) " + todosJogadores[p].nomeJogador + " foram criadas", todosJogadores[p].corJogador);
        }
    }

    public bool trocarJogador;
    private void Update()
    {

        if (trocarJogador == true)
        {

            seguradorJogadorPrincipal.CarregarJogador(todosJogadores[0]);
            seguradorJogadorQualquer.CarregarJogador(todosJogadores[1]);
            if (jogadorAtual == seguradorJogadorPrincipal)
            {
                jogadorAtual = todosJogadores[1];
            }
            else
            {
                jogadorAtual = todosJogadores[0];
            }
            trocarJogador = false;

        }

        bool foiCompleto = turnos[indiceTurno].Executar();

        if (foiCompleto)
        {

            indiceTurno++;
            if (indiceTurno > turnos.Length - 1)
            {
                indiceTurno = 0;
            }
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

}
