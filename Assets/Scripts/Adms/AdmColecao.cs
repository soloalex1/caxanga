using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmColecao : MonoBehaviour
{
    public Baralho baralho = new Baralho();
    public GameObject prefabCarta, paginaEsquerda, paginaDireita;

    GameObject carta;
    ExibirInfoCarta infoCarta;

    InstanciaCarta instCarta;

    AdmRecursos ar = Configuracoes.GetAdmRecursos();

    void Start()
    {
        for(int i = 0; i < baralho.cartasBaralho.Count; i++ )
        {
            carta = Instantiate(prefabCarta) as GameObject;
            infoCarta = carta.GetComponent<ExibirInfoCarta>();
            instCarta = carta.GetComponent<InstanciaCarta>();
            infoCarta.CarregarCarta(ar.obterInstanciaCarta(baralho.cartasBaralho[i]));

            instCarta.carta = infoCarta.carta;
            instCarta.SetPoderECusto();
            infoCarta.CarregarCarta(instCarta.carta); 
        }
    }

    void Update()
    {
        
    }

    void AoClicarBotao(){

    }

}
