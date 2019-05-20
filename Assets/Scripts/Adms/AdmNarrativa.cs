using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmNarrativa : MonoBehaviour
{

    private ArrayList frasesMoacir;
    private ArrayList frasesBoto;
    private ArrayList frasesIara;


    void Start(){
        frasesMoacir = new ArrayList();
        frasesBoto = new ArrayList();
        frasesIara = new ArrayList();

        frasesMoacir.Add(new Dialogo("Moacir"));

    }























    // public GameObject papelMoacir;
    // public GameObject papelOponente;

    // public string personagemAtivo = "";

    // ArrayList falasMoacir = new ArrayList();
    // ArrayList falasBoto = new ArrayList();
    // ArrayList falasIara = new ArrayList();

    // int indexMoacir = 0, indexBoto = 0, indexIara = 0;

    // void Start(){
    //     preencherBoto();
    //     preencherMoacir();
    //     papelMoacir.SetActive(false);
    //     papelOponente.SetActive(false);

    //     personagemAtivo = "Moacir";

    // }

    // void preencherMoacir(){
    //     falasMoacir.Add("moacir1");
    //     falasMoacir.Add("moacir2");
    //     falasMoacir.Add("moacir3");
    //     falasMoacir.Add("moacir4");
    // }

    // void preencherBoto(){
    //     falasBoto.Add("boto1");
    //     falasBoto.Add("boto2");
    //     falasBoto.Add("boto3");
    //     falasBoto.Add("boto4");
    // }

    // public void ProximoDialogo(){
    //     switch(personagemAtivo){
    //         case "Moacir":
    //             indexMoacir += 1;
    //             if(indexMoacir > falasMoacir.Size()){
    //                 indexMoacir = falasMoacir.Size();
    //             }
    //             papelMoacir.SetActive(true);
    //             papelOponente.SetActive(false);
    //             break;

    //         case "Boto":
    //             indexBoto += 1;
    //             papelOponente.SetActive(true);
    //             papelMoacir.SetActive(false);
    //             break;

    //         case "Iara":
    //             indexIara += 1;
    //             papelOponente.SetActive(true);
    //             papelMoacir.SetActive(false);
    //             break;
    //     }
    // }
} 