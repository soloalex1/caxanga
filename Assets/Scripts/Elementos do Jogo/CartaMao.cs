using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ElementosJogo/CartaMao")]
public class CartaMao : LogicaInstanciaCarta
{
    public GameEvent aoSelecionarCartaAtual;
    public VariavelCarta cartaAtual;
    public override void AoClicar(InstanciaCarta c)
    {
        cartaAtual.Set(c);
        aoSelecionarCartaAtual.Raise();
    }
    public override void AoSelecionar(InstanciaCarta c)
    {

    }
}
