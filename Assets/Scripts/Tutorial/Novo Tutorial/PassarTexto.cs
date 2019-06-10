using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassarTexto : MonoBehaviour
{
    public GameEvent passouTexto;
    public void ApertouBotao()
    {
        passouTexto.Raise();
    }
    public void TocarSom(){
        Configuracoes.admTutorial.TocarSomBotao();
    }
}
