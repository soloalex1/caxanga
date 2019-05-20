using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game Event")]
public class GameEvent : ScriptableObject
{
    public InstanciaCarta cartaQueAtivouEvento;
    public List<GameEventListener> listeners;
    public void Register(GameEventListener l)
    {
        listeners.Add(l);
    }

    public void UnRegister(GameEventListener l)
    {
        listeners.Remove(l);
    }

    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].Response();
        }
    }
}

