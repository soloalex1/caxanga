using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizadorPropriedadeUI : GameEventListener
{
    
    public bool raiseOnEnable;
    /// In the off chance you need to update a UI element when disabled, just add the OnDisable() method

    public override void Response()
    {
        if (gameEvent != null)
            Raise();
    }

    public virtual void Raise()
    {
        
    }

    public override void OnEnableLogic()
    {
        base.OnEnableLogic();
        if(raiseOnEnable)
        {
            Raise();
        }
    }
}