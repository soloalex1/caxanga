using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public static class Configuracoes
{
    public static AdmJogo admJogo;
    private static AdmRecursos _admRecursos;
    private static ConsoleHook _admConsole;

    public static void RegistrarEvento(string e, Color color)
    {
        if (_admConsole == null)
        {
            _admConsole = Resources.Load("ConsoleHook") as ConsoleHook;
        }
        _admConsole.RegistrarEvento(e, color);
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
        DefinirPaiCarta(c, p);
        // admJogo.jogadorAtual.PodeUsarCarta(instCarta.infoCarta.carta);
        admJogo.jogadorAtual.BaixarCarta(instCarta);
    }

    public static void DefinirPaiCarta(Transform carta, Transform pai)//essa função é foda... queria ter ela :'(
    {
        carta.SetParent(pai);//define o pai da carta
        //depois faz os ajustes de posição, rotação e de escala
        carta.localPosition = Vector3.zero;
        carta.eulerAngles = pai.eulerAngles;//pra ficar na mesma perspectiva
        carta.localScale = new Vector3(0.3F, 0.3F, 0.3F);
    }
}
