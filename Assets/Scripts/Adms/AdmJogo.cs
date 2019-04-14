using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmJogo : MonoBehaviour
{
    public EstadoJogo estadoAtual;

    private void Update()
    {
        estadoAtual.Tick(Time.deltaTime);
    }
}
