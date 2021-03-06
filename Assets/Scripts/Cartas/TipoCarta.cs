﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipoCarta", menuName = "Cartas/TipoCarta", order = 0)]
public abstract class TipoCarta : ScriptableObject
{
    public string nomeTipo;
    public bool podeAtacar;
    //public logicaTipo logica;
    public virtual void Inicializar(ExibirInfoCarta e)
    {
        Elemento t = Configuracoes.GetAdmRecursos().tipoElemento;
        ExibirInfoPropriedades tipo = e.GetPropriedade(t);
        // tipo.texto.text = nomeTipo;
    }

    public bool DiferenteTipoDeAtacar(InstanciaCarta instCarta)
    {

        if (podeAtacar)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}