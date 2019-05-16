using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartaOlhadaAtual : MonoBehaviour
{

    public VariavelCarta cartaAtual;
    public ExibirInfoCarta infoCarta;
    Vector3 posicao;
    public Sprite cursorIdle, cursorAlvoCinza;

    public void CarregarCartaOlhada()
    {
        infoCarta.CarregarCarta(cartaAtual.valor.infoCarta.carta);
        infoCarta.gameObject.SetActive(true);
    }
    public void FecharCarta()
    {
        infoCarta.gameObject.SetActive(false);
        if (Configuracoes.admJogo.estadoAtual != null && Configuracoes.admJogo.estadoAtual.name == "Usando Efeito")
        {
            Configuracoes.admCursor.MudarSprite(cursorAlvoCinza);
        }
        else
        {
            if (Configuracoes.admCursor.imagemCursor.sprite != cursorIdle)
            {
                Configuracoes.admCursor.MudarSprite(cursorIdle);
            }
        }
    }
    private void Start()
    {
        Vector3 posicao = Vector3.zero;
        FecharCarta();
    }
}
