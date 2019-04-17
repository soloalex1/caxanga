using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmJogo : MonoBehaviour
{
    public EstadoJogador estadoAtual;

    private void Start()
    {
        Configuracoes.admJogo = this;
    }
    private void Update()
    {
        estadoAtual.Tick(Time.deltaTime);
    }

    public void DefinirEstado(EstadoJogador estado)
    {
        estadoAtual = estado;
    }
}
