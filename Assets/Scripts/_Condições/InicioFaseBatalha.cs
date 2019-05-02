using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ações/InicioFaseBatalha")]
public class InicioFaseBatalha : Condicao
{
    public override bool condicaoValida()
    {
        AdmJogo admJogo = AdmJogo.singleton;
        SeguradorDeJogador jogadorAtual = admJogo.jogadorAtual;
        int cont = jogadorAtual.cartasBaixadas.Count;

        for (int i = 0; i < jogadorAtual.cartasBaixadas.Count; i++)
        {
            if (!jogadorAtual.cartasBaixadas[i].podeAtacarNesteTurno)
            {
                cont--;
            }
        }

        if (cont > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
