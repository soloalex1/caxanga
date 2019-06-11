using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdmCena : MonoBehaviour
{
    public GameObject telaConfiguracoes;
    public Transform botoesMenuLivro;
    public Sprite cursorIdle;
    public Transform transformTelaCarregamento;
    public GameObject telaCarregamento, fadeInstanciado;
    // gerenciamento de cenas

    private void Start()
    {
        fadeInstanciado.GetComponent<Animator>().Play(1);
        Configuracoes.admCena = this;
    }
    public void MudarTela(string nomeTela)
    {
        //Começa a função de carregar tela
        StartCoroutine(CarregarTela(nomeTela));
    }

    IEnumerator CarregarTela(string nomeTela)
    {
        //cria uma operação assíncrona para carregar a tela sem travar o jogo
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nomeTela);
        // fadeInstanciado.GetComponent<Animator>().Play(0);
        Instantiate(telaCarregamento, transformTelaCarregamento);
        //enquanto a tela não for carregada
        while (!asyncLoad.isDone)
        {
            //Não faça nada
            yield return null;
        }
        //Depois que a cena for carregada
    }
    public void CarregarCena(string proximaCena)
    {
        if (proximaCena == "Coleção" || proximaCena == "Tela Créditos")
        {
            Configuracoes.tempoMusica = GetComponent<TocarSons>().fonteAudio.time;
        }
        StartCoroutine(CarregarTela(proximaCena));
    }

    public void AbrirConfigurações()
    {
        if (telaConfiguracoes.gameObject.activeSelf)
        {
            FecharConfigurações();
            return;
        }
        if (botoesMenuLivro != null)
        {
            foreach (Transform t in botoesMenuLivro)
            {
                t.GetComponent<Button>().interactable = false;
                t.GetComponent<BotaoOver>().enabled = false;
            }
        }

        telaConfiguracoes.gameObject.SetActive(true);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }
    IEnumerator FecharConfig()
    {
        yield return new WaitForSeconds(0.2f);
        if (botoesMenuLivro != null)
        {
            foreach (Transform t in botoesMenuLivro)
            {
                t.GetComponent<Button>().interactable = true;
                t.GetComponent<BotaoOver>().enabled = true;
            }
        }
        telaConfiguracoes.gameObject.SetActive(false);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }
    public void FecharConfigurações()
    {
        StartCoroutine(FecharConfig());
    }
    public void FecherJogo()
    {
        Application.Quit();
    }
}