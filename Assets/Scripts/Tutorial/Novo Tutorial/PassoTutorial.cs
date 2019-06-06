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
    GameObject modal;
    public VariavelTransform posicaoModal;
    public GameObject seta;
    List<GameObject> setas;
    public int numSetas;
    public VariavelTransform[] posicoesSeta;
    public GameObject[] objetosDestacados;
    List <GameObject> objetosDestacadosNaTela;
    public Carta cartaMostrada;
    public bool jogadorInterage;

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
        modal = Instantiate(prefabModal, posicaoModal.valor);
        modal.transform.Find("Texto").GetComponent<Text>().text = textos[indiceTexto];
        if (objetosDestacados.Length > 0)
        {
            objetosDestacadosNaTela = new List<GameObject>();
            Transform posicaoObjsDestacados = GameObject.Find("/Screen Overlay Canvas/Interface do Usuário/Tutorial/Objetos Destacados").transform;
            foreach (GameObject objDestacado in objetosDestacados)
            {
                objetosDestacadosNaTela.Add(Instantiate(objDestacado, posicaoObjsDestacados));
                if (cartaMostrada != null && objDestacado.GetComponent<ExibirInfoCarta>() != null)
                {
                    objDestacado.GetComponent<ExibirInfoCarta>().carta = cartaMostrada;
                    objDestacado.GetComponent<ExibirInfoCarta>().CarregarCarta(cartaMostrada);
                }
            }            
        }
        if (numSetas > 0 && seta != null && posicoesSeta.Length > 0)
        {
            setas = new List<GameObject>();
            for (int i = 0; i < numSetas; i++)
            {
                setas.Add(Instantiate(seta, posicoesSeta[i].valor));
            }
            
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
        if (modal!=null)
            Destroy(modal);
        if (numSetas > 0 && seta != null && setas.Count > 0)
        {
            for (int i = 0; i < setas.Count; i++)
            {
                Destroy(setas[i]);
            }
        }
        if (objetosDestacadosNaTela.Count > 0)
        {
            foreach (GameObject obj in objetosDestacadosNaTela)
            {
                Destroy(obj.gameObject);
            }
        }
    }
}
