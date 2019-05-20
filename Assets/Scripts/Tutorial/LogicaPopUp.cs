using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaPopUp : MonoBehaviour
{
    GameEvent jogadorPassouTexto;
    public void PassarTextoTutorial()
    {
        jogadorPassouTexto.Raise();
    }

}
