using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizarTextoDaFase : AtualizadorPropriedadeUI
{
    public VariavelFase faseAtual;
    public Text textoAlvo;
    
    public override void Raise()
    {
        textoAlvo.text = faseAtual.valor.nomeFase;
    }
}