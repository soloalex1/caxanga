using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{

    public LogicaArea logica;
    public void AoDropar()
    {
        logica.Executar();
    }
}
