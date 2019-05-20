using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Lógicas/CartaMao")]
public class CartaMao : LogicaInstanciaCarta
{
    public GameEvent aoSelecionarCartaAtual;
    public GameEvent aoDeixarDeOlhar;
    public GameEvent aoOlharCarta;
    public VariavelCarta cartaAtual;
    public EstadoJogador segurandoCarta;
    public Sprite cursorClicavel, cursorIdle;


    public override void AoClicar(InstanciaCarta c)
    {
        cartaAtual.Set(c);
        Configuracoes.admJogo.DefinirEstado(segurandoCarta);
        aoDeixarDeOlhar.Raise();
        aoSelecionarCartaAtual.Raise();
    }
    public override void AoOlhar(InstanciaCarta carta)
    {

        if (carta != cartaAtual.valor && Configuracoes.admJogo.jogadorAtual.cartasMao.Contains(carta))//se for diferente
        {
            cartaAtual.Set(carta);
            aoOlharCarta.Raise();
            if (Configuracoes.admJogo.estadoAtual.name != "Usando Efeito" || Configuracoes.admJogo.estadoAtual.name != "Atacando")
            {
                Configuracoes.admCursor.MudarSprite(cursorClicavel);
            }
            else
            {
                Configuracoes.admCursor.MudarSprite(cursorIdle);
            }
        }
    }
}