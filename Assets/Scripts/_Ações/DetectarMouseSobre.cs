using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Detectar Mouse Sobre")]
public class DetectarMouseSobre : Acao
{

    public GameEvent aoPararDeOlharCarta;
    public VariavelCarta cartaAtual;
    public Sprite cursorClicavel;

    public override void Executar(float d)
    {
        List<RaycastResult> resultados = Configuracoes.GetUIObjs();
        foreach (RaycastResult r in resultados)
        {
            IClicavel carta = r.gameObject.GetComponentInParent<InstanciaCarta>();
            //se acertou algo, mas não é uma carta
            if (carta != null)
            {
                Configuracoes.admCursor.MudarSprite(cursorClicavel);
                carta.AoOlhar();
                return;
            }
        }
        if (resultados.Count <= 0)
        {
            if (cartaAtual.valor != null)
            {
                cartaAtual.valor.gameObject.SetActive(true);
                cartaAtual.valor = null;
            }
            aoPararDeOlharCarta.Raise();
        }
        if (cartaAtual.valor != null)
        {
            cartaAtual.valor.gameObject.SetActive(true);
            cartaAtual.valor = null;
        }
        // aoPararDeOlharCarta.Raise();
        // Debug.Log("Não to com o mouse em nada");

    }
}
