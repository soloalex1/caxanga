using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdmColecao : MonoBehaviour
{
    public Baralho baralho;
    public GameObject prefabCarta, paginaEsquerda, paginaDireita;
    public int numMaxCartasPag;
    public int numPagina = 0;
    public string categoriaAtual = "Todas";
    bool olhandoCarta;
    public GameObject cartaOlhada, telaColecao1, telaColecao2;
    GameObject carta;
    ExibirInfoCarta infoCarta;
    AdmRecursos ar;
    public Sprite cursorClicavel, cursorIdle;
    int peChildCount;
    public GameObject botoesLivro;
    int indice, numCartasPags;

    public Text textoDescricao;
    void Start()
    {
        ar = Configuracoes.GetAdmRecursos();
        InstanciarColecao(categoriaAtual);
        olhandoCarta = false;
    }
    void Update()
    {
        if (!olhandoCarta)
        {
            if (Input.GetMouseButtonDown(0))
            {
                List<RaycastResult> resultados = Configuracoes.GetUIObjs();
                foreach (RaycastResult r in resultados)
                {
                    ExibirInfoCarta c = r.gameObject.GetComponentInParent<ExibirInfoCarta>();
                    if (c != null)
                    {
                        telaColecao1.SetActive(false);
                        telaColecao2.SetActive(true);
                        cartaOlhada.GetComponent<ExibirInfoCarta>().carta = c.carta;
                        cartaOlhada.GetComponent<ExibirInfoCarta>().CarregarCarta(c.carta);
                        textoDescricao.text = c.carta.textoDescricao;
                        olhandoCarta = true;
                        Configuracoes.admCursor.MudarSprite(cursorIdle);
                        Configuracoes.admCursor.sobreBotao = false;
                        break;
                    }
                }
            }
        }

    }
    void InstanciarColecao(string categoriaAtual)
    {
        peChildCount = 0;
        // limpando as páginas

        foreach (Transform i in paginaEsquerda.transform)
        {
            Destroy(i.gameObject);
        }

        foreach (Transform i in paginaDireita.transform)
        {
            Destroy(i.gameObject);
        }
        numCartasPags = 0;
        indice = numPagina * numMaxCartasPag;
        while (indice < baralho.cartasBaralho.Count)
        {
            if (numCartasPags >= 8)
                break;
            if (indice < baralho.cartasBaralho.Count)
            {
                Carta valorCarta = ar.obterInstanciaCarta(baralho.cartasBaralho[indice]);
                if (valorCarta.categoria == categoriaAtual || categoriaAtual == "Todas")
                {
                    carta = Instantiate(prefabCarta) as GameObject;
                    infoCarta = carta.GetComponent<ExibirInfoCarta>();
                    infoCarta.carta = valorCarta;
                    infoCarta.CarregarCarta(valorCarta);
                    carta.AddComponent<BotaoOver>();
                    carta.GetComponent<BotaoOver>().cursorClicavel = cursorClicavel;
                    carta.GetComponent<BotaoOver>().cursorIdle = cursorIdle;
                    // joga as cartas fisicamente na mão do jogador
                    if (peChildCount < 4)
                    {
                        Configuracoes.DefinirPaiCarta(carta.transform, paginaEsquerda.transform);
                        peChildCount++;
                    }
                    else
                    {
                        Configuracoes.DefinirPaiCarta(carta.transform, paginaDireita.transform);
                    }
                    carta.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 1);
                    numCartasPags++;
                }
            }
            else
            {
                break;
            }
            indice++;
        }
    }

    public void Avancar()
    {
        if (numPagina < baralho.cartasBaralho.Count / numMaxCartasPag && numCartasPags == 8)
        {
            numPagina++;
            InstanciarColecao(categoriaAtual);
        }

    }
    public void Voltar()
    {
        if (olhandoCarta)
        {
            telaColecao1.SetActive(true);
            telaColecao2.SetActive(false);
            olhandoCarta = false;
        }
        else
        {
            if (numPagina > 0)
            {
                numPagina--;
                InstanciarColecao(categoriaAtual);
            }

        }

    }
    public void MudarCategoria(string categoria)
    {
        if (olhandoCarta == false)
        {
            foreach (Transform t in botoesLivro.transform)
            {
                t.GetComponent<Image>().color = new Color(255, 255, 255);
            }
            EventSystem m_EventSystem = EventSystem.current;
            m_EventSystem.currentSelectedGameObject.GetComponent<Image>().color = new Color(0.8584906f, 0.8338431f, 0.1822268f);
            categoriaAtual = categoria;
            numPagina = 0;
            InstanciarColecao(categoriaAtual);
        }
    }
}
