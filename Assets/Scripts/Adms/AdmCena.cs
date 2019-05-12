using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdmCena : MonoBehaviour
{

    // gerenciamento de cenas
    public void CarregarCena(string proximaCena)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadSceneAsync(proximaCena, LoadSceneMode.Single);
    }
}