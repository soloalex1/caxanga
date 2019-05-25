using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tutorial/Passo Tutorial")]
public class PassoTutorial : ScriptableObject
{
    GameEvent eventoFinalizador;
    public string[] textos;
    public int indiceTexto = 0;
    public GameObject[] modais;
    public GameObject[] objetosDestacados;
    public bool turnoJogador;

    public void AoIniciar()
    {
        for (int i = 0; i < modais.Length; i++)
        {
            if (modais[i] != null)
            {
                modais[i].SetActive(true);
            }
        }
        //Somente se houver mais de um modal ao mesmo tempo
        if (modais.Length > 1)
        {
            for (int i = 0; i < modais.Length; i++)
            {
                if (modais[i] != null)
                {
                    modais[i].transform.Find("Texto").GetComponent<Text>().text = textos[i];
                }
            }
        }
        else
        {
            modais[0].transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
        }
    }
}
