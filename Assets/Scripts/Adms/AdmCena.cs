using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdmCena : MonoBehaviour
{

    // gerenciamento de cenas
    public void CarregarCena(string proximaCena)
    {
        // loadSceneAsync tá demorando mais
        SceneManager.LoadScene(proximaCena, LoadSceneMode.Single);
    }
}