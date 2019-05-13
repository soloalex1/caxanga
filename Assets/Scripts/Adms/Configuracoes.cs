using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Configuracoes
{
    public static AdmJogo admJogo;
    private static AdmRecursos _admRecursos;

    public static admCursor admCursor;
    private static ConsoleHook _admConsole;
    public static void RegistrarEvento(string e, Color color)
    {
        // if (_admConsole == null)
        // {
        //     _admConsole = Resources.Load("ConsoleHook") as ConsoleHook;
        // }
        // _admConsole.RegistrarEvento(e, color);
    }

    public static AdmRecursos GetAdmRecursos()
    {
        if (_admRecursos == null)
        {
            _admRecursos = Resources.Load("AdmRecursos") as AdmRecursos;
            _admRecursos.AoIniciar();
        }
        return _admRecursos;
    }

    public static List<RaycastResult> GetUIObjs()
    {
        PointerEventData dadosDoPonto = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        List<RaycastResult> resultados = new List<RaycastResult>();
        EventSystem.current.RaycastAll(dadosDoPonto, resultados);
        return resultados;
    }

    public static void BaixarCartaLenda(Transform c, Transform p, InstanciaCarta instCarta)
    {

        instCarta.podeAtacarNesteTurno = false;
        //Aqui a gente vai executar os efeitos das cartas, bem como as diferenças em carta e feitiço
        if (instCarta.podeAtacarNesteTurno == false)
        {
            instCarta.gameObject.transform.Find("Frente da Carta").GetComponent<Image>().sprite = instCarta.infoCarta.spriteNaoPodeAtacar;
        }
        DefinirPaiCarta(c, p);
        if (instCarta.infoCarta.carta.efeito != null)
        {
            Efeito novoEfeito = ScriptableObject.CreateInstance("Efeito") as Efeito;
            novoEfeito = instCarta.infoCarta.carta.efeito;
            instCarta.efeito = novoEfeito;
            instCarta.efeito.cartaQueInvoca = instCarta;
            instCarta.efeito.jogadorQueInvoca = admJogo.jogadorAtual;
        }
        admJogo.jogadorAtual.BaixarCarta(instCarta);
    }
    public static void DefinirPaiCarta(Transform carta, Transform pai)//essa função é foda... queria ter ela :'(
    {
        carta.SetParent(pai);//define o pai da carta
        //depois faz os ajustes de posição, rotação e de escala
        carta.localPosition = Vector3.zero;
        carta.eulerAngles = pai.eulerAngles;//pra ficar na mesma perspectiva
        carta.localScale = new Vector3(0.28F, 0.28F, 0.28F);
    }
}
