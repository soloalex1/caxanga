using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanciaCarta : MonoBehaviour, IClicavel
{
    public Efeito efeito;
    public bool efeitoUsado = false;
    public bool podeSofrerEfeito = true;
    public LogicaInstanciaCarta logicaAtual;
    public ExibirInfoCarta infoCarta;
    public bool podeAtacarNesteTurno;
    public bool podeSerAtacada;
    public bool PodeAtacar()
    {
        bool resultado = true;
        if (podeAtacarNesteTurno == false)
        {
            resultado = false;
        }
        if (infoCarta.carta.tipoCarta.DiferenteTipoDeAtacar(this))
        {
            resultado = true;
        }
        return resultado;
    }
    void Start()
    {
        infoCarta = GetComponent<ExibirInfoCarta>();
    }

    void IClicavel.AoClicar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoClicar(this);
        }
    }

    void IClicavel.AoSelecionar()
    {
        if (logicaAtual != null)
        {
            logicaAtual.AoSelecionar(this);
        }
    }
}
