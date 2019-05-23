using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameEventListener : MonoBehaviour
{
    public List<GameEvent> gameEvents;
    public UnityEvent response;

    /// <summary>
    /// Override this to override the OnEnableLogic()
    /// </summary>

    private void Start()
    {
    }
    public virtual void OnEnableLogic()
    {
        if (gameEvents.Count > 0)
        {
            foreach (GameEvent e in gameEvents)
            {
                e.Register(this);
            }
        }
        // if (gameEvent != null)
        //     gameEvent.Register(this);
    }

    void OnEnable()
    {
        OnEnableLogic();
    }

    /// <summary>
    /// Override this to override the OnDisableLogic()
    /// </summary>
    public virtual void OnDisableLogic()
    {
        if (gameEvents.Count > 0)
        {
            foreach (GameEvent e in gameEvents)
            {
                e.UnRegister(this);
            }
        }
        // if (gameEvent != null)
        //gameEvent.UnRegister(this);
    }

    void OnDisable()
    {
        OnDisableLogic();
    }

    public virtual void Response()
    {
        response.Invoke();
    }
}

