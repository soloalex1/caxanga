using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Ações/Ações do Jogador/Ativar Ataque das Lendas")]
public class AtivarAtaqueLendas : AcaoJogador
{
    public override void Executar(SeguradorDeJogador jogador)
    {
        foreach (InstanciaCarta instCarta in jogador.cartasBaixadas)
        {
            if (instCarta.podeAtacarNesteTurno == false)
            {
                instCarta.podeAtacarNesteTurno = true;
                instCarta.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = instCarta.infoCarta.spritePodeAtacar;
            }
        }
    }
}
