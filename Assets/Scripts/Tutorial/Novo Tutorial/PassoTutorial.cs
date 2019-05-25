using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Tutorial/Passo Tutorial")]
public class PassoTutorial : ScriptableObject
{
    public GameEvent eventoFinalizador;
    public string[] textos;
    public int indiceTexto = 0;
    public GameObject prefabModal;
    public GameObject modal;
    public VariavelTransform posicaoModal;
    public GameObject[] objetosDestacados;
    public bool turnoJogador;

    public void AoIniciar()
    {
        indiceTexto = 0;
        Instantiate(prefabModal, posicaoModal.valor);
        prefabModal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
        modal = posicaoModal.valor.Find("PopUpNormal(Clone)").gameObject;
    }
    public void AtualizarTexto()
    {
        Debug.Log("Vou atualizar o texto");
        // modal = GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Passo 1 Modal/PopUpNormal(Clone)").gameObject;
        modal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
    }
}
