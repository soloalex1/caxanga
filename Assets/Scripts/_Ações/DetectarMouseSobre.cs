using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Detectar Mouse Sobre")]
public class DetectarMouseSobre : Acao
{
    public GameEvent aoOlharCarta;
    public GameEvent aoPararDeOlharCarta;
    public VariavelCarta cartaAtual;
    public override void Executar(float d)
    {
        if (cartaAtual.valor != null)
        {
            cartaAtual.valor.gameObject.SetActive(true);
            cartaAtual.valor = null;
            aoPararDeOlharCarta.Raise();
        }
        List<RaycastResult> resultados = Configuracoes.GetUIObjs();
        InstanciaCarta carta;
        foreach (RaycastResult r in resultados)
        {
            carta = r.gameObject.GetComponentInParent<InstanciaCarta>();
            //se acertou algo, mas não é uma carta
            if (carta == null)
            {
                // Debug.Log(cartaAtual.valor.infoCarta.carta.name);
                if (cartaAtual.valor != null)
                {
                    cartaAtual.valor.gameObject.SetActive(true);
                    cartaAtual.valor = null;
                    aoPararDeOlharCarta.Raise();
                    return;
                }

            }
            else//se for uma carta
            {
                if (carta != cartaAtual)//se for diferente
                {
                    cartaAtual.Set(carta);
                    aoOlharCarta.Raise();
                    return;
                    // c.AoSelecionar();
                }
            }
        }
        if (cartaAtual.valor != null)
        {
            cartaAtual.valor.gameObject.SetActive(true);
            cartaAtual.valor = null;
            aoPararDeOlharCarta.Raise();
        }
    }
}
