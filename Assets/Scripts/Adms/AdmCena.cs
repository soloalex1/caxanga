using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdmCena : MonoBehaviour
{
    public GameObject telaConfiguracoes;
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
        telaConfiguracoes.gameObject.SetActive(true);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }

    public void FecharConfigurações()
    {
        telaConfiguracoes.gameObject.SetActive(false);
        GetComponent<admCursor>().MudarSprite(cursorIdle);
    }
    public void FecherJogo()
    {
        Application.Quit();
    }
}