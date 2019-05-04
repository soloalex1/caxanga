using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdmConsole : MonoBehaviour
{
    public Transform gridConsole;
    public GameObject prefab;
    Text[] objetosTexto;
    int indice;

    public ConsoleHook hook;
    private void Awake()
    {
        hook.admConsole = this;
        objetosTexto = new Text[5];
        for (int i = 0; i < objetosTexto.Length; i++)
        {
            GameObject go = Instantiate(prefab) as GameObject;
            objetosTexto[i] = go.GetComponent<Text>();
            go.transform.SetParent(gridConsole);
        }
    }
    public void RegistrarEvento(string s, Color color)
    {
        indice++;
        if (indice > objetosTexto.Length - 1)
        {
            indice = 0;
        }
        objetosTexto[indice].color = color;
        objetosTexto[indice].text = s;
        objetosTexto[indice].gameObject.SetActive(true);
        objetosTexto[indice].transform.SetAsLastSibling();
    }
}
