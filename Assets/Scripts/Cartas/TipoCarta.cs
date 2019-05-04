using System.Collections;
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
        /*
        por exemplo: Tipo 'investida' pode atacar no primeiro turno que for colocado

        bool r = logica.Executar(instCarta) 

        logica.Executar(instCarta c)
        {
            if (!c.podeAtacarNesteTurno) 
            {
                c.podeAtacarNesteTurno = true;
            }
            return c.podeAtacarNesteTurno;
        }

        */

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