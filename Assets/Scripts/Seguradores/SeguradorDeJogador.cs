using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Seguradores/Segurador de Jogador")]
public class SeguradorDeJogador : ScriptableObject
{
    public EstadoJogador usandoEfeito;
    public Rodada rodada;
    public bool protegido, silenciado, fezAlgumaAcao;
    public bool passouRodada;
    public Sprite moldura;
    public GameEvent cartaEntrouEmCampo, cartaMorreu;
    public bool podeUsarEfeito = true;
    public int numCartasMaoInicio;
    public Baralho baralho = null;
    public Baralho baralhoInicial;
    public Color corJogador;
    public Sprite retratoJogador;
    public string nomeJogador;
    public int vidaInicial, magiaInicial;
    public int magia;
    public int vida;
    public InfoUIJogador infoUI;
    public bool jogadorHumano;
    public bool podeSerAtacado;
    public int barrasDeVida;

    [System.NonSerialized]
    public SeguradorDeCartas seguradorCartas;
    public int lendasBaixadasNoTurno;
    public int feiticosBaixadosNoTurno;
    public int maxFeiticosTurno;
    public int maxLendasTurno;

    public Sprite textoTurnoImage;
    public LogicaInstanciaCarta logicaMao;
    public LogicaInstanciaCarta logicaBaixada;
    public LogicaInstanciaCarta logicaCemiterio;


    [System.NonSerialized] // não precisa serializar porque é selfdata (Não entendi nesse momento ainda, quem sabe qnd eu terminar toda a lógica)
    public List<InstanciaCarta> cartasMao = new List<InstanciaCarta>(); // lista de cartas na mão do jogador em questão
    [System.NonSerialized]
    public List<InstanciaCarta> cartasBaixadas = new List<InstanciaCarta>(); // lista de cartas no campo do jogador em questão
    public List<InstanciaCarta> cartasCemiterio = new List<InstanciaCarta>(); // lista de cartas no cemitério

    public void InicializarJogador()
    {
        magia = magiaInicial;
        vida = vidaInicial;
        barrasDeVida = 3;
        baralho = ScriptableObject.CreateInstance("Baralho") as Baralho;
        baralho.cartasBaralho = new List<string>();
        cartasCemiterio.Clear();
        lendasBaixadasNoTurno = 0;
        feiticosBaixadosNoTurno = 0;
        podeUsarEfeito = true;
        podeSerAtacado = true;
        passouRodada = false;
        fezAlgumaAcao = false;


        if (this == Configuracoes.admJogo.jogadorLocal)
        {
            infoUI = Configuracoes.admJogo.infoJogadorLocal;
        }
        else
        {
            infoUI = Configuracoes.admJogo.infoJogadorIA;
        }
        foreach (string carta in baralhoInicial.cartasBaralho)
        {
            baralho.cartasBaralho.Add(carta);
        }

        if (jogadorHumano == true)
        {
            seguradorCartas = Configuracoes.admJogo.seguradorCartasJogadorLocal;
        }
        else
        {
            seguradorCartas = Configuracoes.admJogo.seguradorCartasJogadorInimigo;
        }
        CarregarInfoUIJogador();
        PuxarCartasIniciais();
    }

    public void BaixarCarta(InstanciaCarta instCarta)
    {
        if (cartasMao.Contains(instCarta))
        {
            cartasMao.Remove(instCarta);
        }
        cartasBaixadas.Add(instCarta);
        fezAlgumaAcao = true;
        Configuracoes.RegistrarEvento(nomeJogador + " baixou a carta " + instCarta.infoCarta.carta.name + " de custo " + instCarta.infoCarta.carta.AcharPropriedadePeloNome("Custo").intValor, corJogador);
        infoUI.AtualizarMagia();

        if (instCarta.efeito != null
        && instCarta.efeito.eventoAtivador.name == "Carta Entrou Em Campo"
        && instCarta.jogadorDono.podeUsarEfeito)
        {
            if (instCarta.efeito.apenasCarta && instCarta.efeito.alteracaoPoder < 0 && Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Count == 0)
            {
                return;
            }
            if (instCarta.efeito.afetaTodasCartas)
            {
                Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.ExecutarEfeito(instCarta.efeito));
            }
            Configuracoes.admJogo.efeitoAtual = instCarta.efeito;
            Configuracoes.admJogo.DefinirEstado(usandoEfeito);
        }

    }
    public bool PodeUsarCarta(Carta c)
    {
        bool resultado = false;
        Propriedades custo = c.AcharPropriedadePeloNome("Custo");
        if (c != null && magia >= custo.intValor)
        {
            magia -= custo.intValor;
            resultado = true;
        }
        if (resultado == false)
        {
            Configuracoes.RegistrarEvento("Você não tem magia o suficiente para baixar esta carta", Color.white);
        }
        return resultado;
    }
    public void CarregarInfoUIJogador()
    {
        if (infoUI != null)
        {
            infoUI.jogador = this;
            infoUI.AtualizarTudo();
        }
    }

    public void PuxarCartasIniciais()
    {

        baralho.Embaralhar();
        for (int i = 0; i < numCartasMaoInicio; i++)
        {
            Configuracoes.admJogo.PuxarCarta(this);
        }

        foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasMao)
        {
            if (c != null)
            {
                c.transform.Find("Fundo da Carta").gameObject.SetActive(true);
            }
        }
    }
    public void ColocarCartaNoCemiterio(InstanciaCarta carta)
    {
        cartasCemiterio.Add(carta);
        carta.logicaAtual = logicaCemiterio;
        carta.transform.SetParent(seguradorCartas.gridCemiterio.valor, false);
        carta.transform.Find("Sombra").gameObject.SetActive(true);
        carta.transform.Find("Fundo da Carta").gameObject.SetActive(false);
        carta.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
        carta.transform.Find("Sombra").GetComponent<Image>().color = new Color(0, 0, 0, 0.7F);
        if (carta.efeito != null && carta.efeito.eventoAtivador == cartaMorreu)
        {
            Configuracoes.admJogo.StartCoroutine("ExecutarEfeito", carta.efeito);
        }
        Vector3 posicao = Vector3.zero;
        posicao.x = cartasCemiterio.Count * 10;
        posicao.z = cartasCemiterio.Count * 10;

        carta.transform.localPosition = posicao;
        carta.transform.localRotation = Quaternion.identity;
        carta.transform.localScale = Vector3.one;

        carta.gameObject.SetActive(true);

    }
}
