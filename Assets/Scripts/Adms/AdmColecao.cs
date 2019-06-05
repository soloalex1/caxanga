using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmColecao : MonoBehaviour
{
    public Baralho baralho;
    public GameObject prefabCarta, paginaEsquerda, paginaDireita;

    int numPagina = 0;
    GameObject carta;
    ExibirInfoCarta infoCarta;

    InstanciaCarta instCarta;

    AdmRecursos ar;

    void Start()
    {
        ar = Configuracoes.GetAdmRecursos();
        InstanciarColecao(0);
    }

    void InstanciarColecao(int index){

        for(int i = index; i < (index + 8); i++ )
        {
            carta = Instantiate(prefabCarta) as GameObject;
            infoCarta = carta.GetComponent<ExibirInfoCarta>();
            instCarta = carta.GetComponent<InstanciaCarta>();
            infoCarta.CarregarCarta(ar.obterInstanciaCarta(baralho.cartasBaralho[i]));
            
            Debug.Log("carreguei a carta " + baralho.cartasBaralho[i].ToString()); 

            instCarta.carta = infoCarta.carta;
            instCarta.SetPoderECusto();
            infoCarta.CarregarCarta(instCarta.carta);

            // joga as cartas fisicamente na mão do jogador
            if((i - index) < 4){
                Configuracoes.DefinirPaiCarta(carta.transform, paginaEsquerda.transform);
            } else {
                numPagina++;
                Configuracoes.DefinirPaiCarta(carta.transform, paginaDireita.transform);
            }
            carta.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }
    }
    void Update()
    {
        
    }

    void AoClicarBotao(){

    }

}
