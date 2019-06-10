using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Seguradores/Segurador de Jogador")]
public class SeguradorDeJogador : ScriptableObject
{
    public GameEvent jogouBoiuna, jogouBoitata;
    public EstadoJogador usandoEfeito;
    public Rodada rodada;
    public bool protegido, silenciado, fezAlgumaAcao;
    public bool passouRodada;
    public Sprite moldura;
    public GameEvent cartaEntrouEmCampo, cartaMorreu;
    public bool podeUsarEfeito = true;
    public int numCartasMaoInicio;
    [HideInInspector]
    public Baralho baralho;
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
    [System.NonSerialized]
    public List<InstanciaCarta> cartasCemiterio = new List<InstanciaCarta>(); // lista de cartas no cemitério

    public void InicializarJogador()
    {
        magia = magiaInicial;
        vida = vidaInicial;
        barrasDeVida = 3;
        baralho = ScriptableObject.CreateInstance("Baralho") as Baralho;
        baralho.cartasBaralho = new List<string>();
        baralho.jogador = this;
        cartasCemiterio.Clear();
        lendasBaixadasNoTurno = 0;
        feiticosBaixadosNoTurno = 0;
        podeUsarEfeito = true;
        podeSerAtacado = true;
        passouRodada = false;
        fezAlgumaAcao = false;
        silenciado = false;

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



    public void BaixarCarta(Transform c, Transform p, InstanciaCarta instCarta)
    {
        if (cartasMao.Contains(instCarta))
        {
            cartasMao.Remove(instCarta);
        }
        cartasBaixadas.Add(instCarta);  
        fezAlgumaAcao = true;
        magia -= instCarta.custo;
        infoUI.AtualizarMagia();
        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.DestacarCartaBaixada(instCarta));

        instCarta.podeAtacarNesteTurno = false;
        //Aqui a gente vai executar os efeitos das cartas, bem como as diferenças em carta e feitiço
        if (instCarta.podeAtacarNesteTurno == false)
        {
            instCarta.gameObject.transform.Find("Sombra").gameObject.SetActive(true);
            instCarta.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = instCarta.infoCarta.spriteNaoPodeAtacar;
        }
        Configuracoes.DefinirPaiCarta(c, p);
        cartaEntrouEmCampo.cartaQueAtivouEvento = instCarta;
        Configuracoes.admEfeito.eventoAtivador = cartaEntrouEmCampo;
        cartaEntrouEmCampo.Raise();
        if (Configuracoes.admJogo.tutorial && instCarta.carta.name == "Boiuna")
        {
            jogouBoiuna.Raise();
        }
        if (Configuracoes.admJogo.tutorial && instCarta.carta.name == "Boitatá")
        {
            jogouBoitata.Raise();
        }
        Configuracoes.admJogo.TocarSomCartaBaixada();
        return;
    }
    public bool TemMagiaParaBaixarCarta(InstanciaCarta c)
    {
        bool resultado = false;
        if (magia >= c.custo)
        {
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
        if (Configuracoes.admJogo.tutorial == false)
        {
            if (Configuracoes.admJogo.tutorial == false){
                baralho.Embaralhar();
            }
        }
        else
        {
            numCartasMaoInicio = 5;
        }
        for (int i = 0; i < numCartasMaoInicio; i++)
        {
            Configuracoes.admJogo.PuxarCarta(this);
        }

        // foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasMao)
        // {
        //     if (c != null)
        //     {
        //         c.transform.Find("Fundo da Carta").gameObject.SetActive(true);
        //     }
        // }
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
        cartaMorreu.cartaQueAtivouEvento = carta;
        Configuracoes.admEfeito.eventoAtivador = cartaMorreu;
        cartaMorreu.Raise();
        Vector3 posicao = Vector3.zero;
        posicao.x = cartasCemiterio.Count * 10;
        posicao.z = cartasCemiterio.Count * 10;

        carta.transform.localPosition = posicao;
        carta.transform.localRotation = Quaternion.identity;
        carta.transform.localScale = Vector3.one;

        carta.gameObject.SetActive(true);

    }
}
