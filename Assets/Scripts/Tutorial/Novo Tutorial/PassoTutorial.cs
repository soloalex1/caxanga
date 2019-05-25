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
    public GameObject[] objetosDestacadosNaTela;
    public Carta cartaMostrada;
    public bool turnoJogador;

    public bool jogadorInterage;
    public int indiceObjDestacado;

    public void AoIniciar()
    {
        indiceTexto = 0;
        indiceObjDestacado = 0;
        objetosDestacadosNaTela = new GameObject[objetosDestacados.Length];
        Instantiate(prefabModal, posicaoModal.valor);
        modal = posicaoModal.valor.Find(prefabModal.name + "(Clone)").gameObject;
        modal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
        if (objetosDestacados.Length > 0)
        {
            if (cartaMostrada != null)
            {
                objetosDestacados[indiceObjDestacado].GetComponent<ExibirInfoCarta>().carta = cartaMostrada;
                objetosDestacados[indiceObjDestacado].GetComponent<ExibirInfoCarta>().CarregarCarta(cartaMostrada);
            }
            Instantiate(objetosDestacados[indiceObjDestacado], GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Objeto Destacado").transform);
            objetosDestacadosNaTela[indiceObjDestacado] = GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Objeto Destacado");
        }
        Configuracoes.admJogo.pause = false;
        GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Fundo").SetActive(false);

    }
    public void AtualizarTexto()
    {
        // modal = GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Passo 1 Modal/PopUpNormal(Clone)").gameObject;
        modal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
    }
    public void FinalizarPasso()
    {
        if (objetosDestacados.Length > 0)
        {
            Debug.Log(indiceObjDestacado);
            Destroy(objetosDestacadosNaTela[indiceObjDestacado].transform.GetChild(indiceObjDestacado).gameObject);
        }
        modal.SetActive(false);
    }
}
