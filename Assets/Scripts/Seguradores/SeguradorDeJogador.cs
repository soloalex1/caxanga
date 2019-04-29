using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Seguradores/Segurador de Jogador")]
public class SeguradorDeJogador : ScriptableObject
{
    public string nomeJogador;
    public int magia;
    public int vida;
    public bool jogadorHumano;
    public string[] cartasMaoInicio;

    [System.NonSerialized]
    public SeguradorDeCartas seguradorAtual;

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
        return resultado;
    }
}
