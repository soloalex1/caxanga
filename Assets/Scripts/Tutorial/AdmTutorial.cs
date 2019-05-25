using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmTutorial : MonoBehaviour
{
    public PassoTutorial[] passos;
    public PassoTutorial passoAtual;
    GameEventListener gmListener;
    private void Start()
    {
        gmListener = GetComponent<GameEventListener>();
        for (int i = 0; i < passos.Length; i++)
        {
            gmListener.gameEvents.Add(passos[i].eventoFinalizador);
        }
        passoAtual = passos[0];
        passoAtual.AoIniciar();
    }
    public void ativouEvento()
    {
        Debug.Log("Eu ouvi isso hein");
        if (passoAtual.indiceTexto != passoAtual.textos.Length - 1)
        {
            passoAtual.indiceTexto++;
            passoAtual.AtualizarTexto();
        }
    }
}
