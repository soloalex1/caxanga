using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite cursorIdle, cursorClicavel;

    void Start()
    {
        GetComponent<AudioSource>().volume = Configuracoes.volumeSFX;
    }
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

    public void TocarSom()
    {
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
    }
    public void voltarCursor()
    {
        Configuracoes.admCursor.MudarSprite(cursorIdle);
        Configuracoes.admCursor.sobreBotao = false;
    }
}
