using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizarTexto : AtualizadorPropriedadeUI
{
    public VariavelString stringAlvo;
    public Text textoAlvo;
    
    public override void Raise()
    {
        textoAlvo.text = stringAlvo.valor;
    }
    
    public void Raise(string alvo)
    {
        textoAlvo.text = alvo;
    }
}