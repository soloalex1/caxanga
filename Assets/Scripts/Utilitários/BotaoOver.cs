using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite cursorIdle, cursorClicavel;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Configuracoes.admCursor.MudarSprite(cursorClicavel);
        Configuracoes.admCursor.sobreBotao = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Configuracoes.admCursor.MudarSprite(cursorIdle);
        Configuracoes.admCursor.sobreBotao = false;
    }
}
