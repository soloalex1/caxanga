using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogo {

    public string nomePersonagem;
    public ArrayList frases;

    public Dialogo(string nome){
        nomePersonagem = nome;
        frases = new ArrayList();
    }

    public void PreencherDialogos(string frase){
        frases.Add(frase);
    }
}