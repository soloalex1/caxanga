using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmTutorial : MonoBehaviour
{
    public PassoTutorial[] passos;
    public PassoTutorial passoAtual;
    int indicePasso;
    GameEventListener gmListener;

    private void Start()
    {
        indicePasso = 0;
        passoAtual = passos[indicePasso];
        passoAtual.AoIniciar();
    }
    public void ativouEvento()
    {
        if (passoAtual.indiceTexto != passoAtual.textos.Length - 1)
        {
            if (passoAtual.objetosDestacados.Length > 1)
            {
                passoAtual.objetosDestacadosNaTela[passoAtual.indiceObjDestacado].SetActive(false);
                passoAtual.indiceObjDestacado++;
                Instantiate(passoAtual.objetosDestacados[passoAtual.indiceObjDestacado], this.transform);
                passoAtual.objetosDestacadosNaTela[passoAtual.indiceObjDestacado] = passoAtual.objetosDestacados[passoAtual.indiceObjDestacado];
            }
            passoAtual.indiceTexto++;
            passoAtual.AtualizarTexto();
            return;
        }
        else
        {
            passoAtual.FinalizarPasso();
            indicePasso++;
            if (passos.Length > indicePasso)
            {
                passoAtual = passos[indicePasso];
                passoAtual.AoIniciar();
            }
        }
    }
}
