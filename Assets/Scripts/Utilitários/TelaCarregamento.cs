using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelaCarregamento : MonoBehaviour
{
    public GameObject cartaOlhada;
    public Text textoDescricao;
    public Sprite cursorIdle;
    public Baralho baralhoCarregamento;
    void Start()

    {
        AdmRecursos ar = Configuracoes.GetAdmRecursos();
        Carta c = ar.obterInstanciaCarta(baralhoCarregamento.cartasBaralho[Random.Range(0, baralhoCarregamento.cartasBaralho.Count)]);
        cartaOlhada.GetComponent<ExibirInfoCarta>().carta = c;
        cartaOlhada.GetComponent<ExibirInfoCarta>().CarregarCarta(c);
        textoDescricao.text = c.textoDescricao;
        Configuracoes.admCursor.MudarSprite(cursorIdle);
        Configuracoes.admCursor.sobreBotao = false;
    }
}
