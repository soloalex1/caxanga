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
    public bool jogadorInterage;
    public int indiceObjDestacado;

    public IEnumerator AoIniciar()
    {
        if (GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Carta Sendo Olhada").transform.GetChild(0) != null)
        {
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Carta Sendo Olhada").transform.GetChild(0).gameObject.SetActive(false);
        }
        if (Configuracoes.turnoDaIATutorial)
        {
            yield return new WaitUntil(() => Configuracoes.turnoDaIATutorial == false);
        }

        if (jogadorInterage)
        {
            Configuracoes.admJogo.pause = false;
        }
        else
        {
            Configuracoes.admJogo.pause = true;
        }

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
        if (jogadorInterage == false)
        {
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Fundo").SetActive(true);
        }
        else
        {
            GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Fundo").SetActive(false);
        }
        yield return null;
    }
    public void ProximoTexto()
    {
        indiceTexto++;
        modal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
    }
    public void FinalizarPasso()
    {
        Destroy(modal);
        if (objetosDestacados.Length > 0)
        {
            Destroy(objetosDestacadosNaTela[indiceObjDestacado].transform.GetChild(indiceObjDestacado).gameObject);
        }
    }
}
