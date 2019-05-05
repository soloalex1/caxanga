using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seguradores/Segurador de Jogador")]
public class SeguradorDeJogador : ScriptableObject
{
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
    public string[] cartasMaoInicio;

    [System.NonSerialized]
    public SeguradorDeCartas seguradorCartasAtual;


    public int lendasBaixadasNoTurno;
    public int maxLendasTurno;
    public LogicaInstanciaCarta logicaMao;
    public LogicaInstanciaCarta logicaBaixada;

    [System.NonSerialized]//não precisa serializar porque é selfdata (Não entendi nesse momento ainda, quem sabe qnd eu terminar toda a lógica)
    public List<InstanciaCarta> cartasMao = new List<InstanciaCarta>();//lista de cartas na mão do jogador em questão
    [System.NonSerialized]
    public List<InstanciaCarta> cartasBaixadas = new List<InstanciaCarta>();//lista de cartas no campo do jogador em questão
    public void BaixarCarta(InstanciaCarta instCarta)
    {
        if (cartasMao.Contains(instCarta))
        {
            cartasMao.Remove(instCarta);
        }
        cartasBaixadas.Add(instCarta);
        Configuracoes.RegistrarEvento(nomeJogador + " baixou a carta " + instCarta.infoCarta.carta.name + " de custo " + instCarta.infoCarta.carta.AcharPropriedadePeloNome("Custo").intValor, corJogador);
        infoUI.AtualizarMagia();
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
            Configuracoes.RegistrarEvento("Você não tem magia o suficiente para baixar esta Lenda", Color.white);
        }
        return resultado;
    }
    public void LevarDano(int dano)
    {
        vida -= dano;
        if (infoUI != null)
            infoUI.AtualizarVida();
    }
    public void CarregarInfoUIJogador()
    {
        if (infoUI != null)
        {
            infoUI.jogador = this;
            infoUI.AtualizarTudo();
        }
    }
}
