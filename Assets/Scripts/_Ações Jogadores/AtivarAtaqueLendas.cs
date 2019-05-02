using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ações/Ações do Jogador/Ativar Ataque das Lendas")]
public class AtivarAtaqueLendas : AcaoJogador
{
    public override void Executar(SeguradorDeJogador jogador)
    {
        foreach (InstanciaCarta instCarta in jogador.cartasBaixadas)
        {
            if (instCarta.podeAtacar == false)
            {
                instCarta.podeAtacar = true;
                instCarta.transform.Find("Sombra").gameObject.SetActive(false);
            }
        }
    }
}
