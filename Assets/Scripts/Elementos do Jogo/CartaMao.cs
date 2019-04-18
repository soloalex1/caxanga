using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ElementosJogo/CartaMao")]
public class CartaMao : LogicaInstanciaCarta
{
    public GameEvent aoSelecionarCartaAtual;
    public VariavelCarta cartaAtual;
    public EstadoJogador segurandoCarta;
    public override void AoClicar(InstanciaCarta c)
    {
        cartaAtual.Set(c);
        Configuracoes.admJogo.DefinirEstado(segurandoCarta);
        aoSelecionarCartaAtual.Raise();
    }
    public override void AoSelecionar(InstanciaCarta c)
    {

    }
}
