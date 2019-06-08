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

    int dirPagina;

    void Start()
    {
        ar = Configuracoes.GetAdmRecursos();
        dirPagina = 0;
        InstanciarColecao(dirPagina);
    }

    void InstanciarColecao(int index){

        // limpando as páginas

        foreach (Transform i in paginaEsquerda.transform)
        {
            GameObject.Destroy(i.gameObject);   
        }

        foreach (Transform i in paginaDireita.transform)
        {
            GameObject.Destroy(i.gameObject);   
        }

        for(int i = index; i < (index + 8); i++ )
        {
            carta = Instantiate(prefabCarta) as GameObject;
            infoCarta = carta.GetComponent<ExibirInfoCarta>();
            instCarta = carta.GetComponent<InstanciaCarta>();
            infoCarta.CarregarCarta(ar.obterInstanciaCarta(baralho.cartasBaralho[i]));
    
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

    public void AoClicarBotao(int i){
        if(i % 2 == 0){
            if(i >= 8) InstanciarColecao(i - 8);
            else InstanciarColecao(0);
        } else {
            InstanciarColecao(i + 8);
        }
    }

    public void AoClicarCarta(){

    }
}
