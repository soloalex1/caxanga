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
    // gerenciamento de cenas

    private void Start()
    {
        Configuracoes.admCena = this;
    }

    public void CarregarCena(string proximaCena)
    {
        // loadSceneAsync tá demorando mais
        SceneManager.LoadScene(proximaCena, LoadSceneMode.Single);
    }

    public void AbrirConfigurações()
    {
        foreach (Transform t in botoesMenuLivro)
        {
            t.GetComponent<Button>().interactable = false;
            t.GetComponent<BotaoOver>().enabled = false;
        }
        telaConfiguracoes.gameObject.SetActive(true);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }

    public void FecharConfigurações()
    {
        foreach (Transform t in botoesMenuLivro)
        {
            t.GetComponent<Button>().interactable = true;
            t.GetComponent<BotaoOver>().enabled = true;
        }
        telaConfiguracoes.gameObject.SetActive(false);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }
    public void FecherJogo()
    {
        Application.Quit();
    }
}