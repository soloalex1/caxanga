using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Ações/Detectar Mouse Sobre")]
public class DetectarMouseSobre : Acao
{

    public GameEvent aoPararDeOlharCarta;
    public VariavelCarta cartaAtual;
    public Sprite cursorIdle;

    public override void Executar(float d)
    {
        List<RaycastResult> resultados = Configuracoes.GetUIObjs();
        foreach (RaycastResult r in resultados)
        {
            InfoUIJogador infoJogadorAlvo = r.gameObject.GetComponentInParent<InfoUIJogador>();
            IClicavel carta = r.gameObject.GetComponentInParent<InstanciaCarta>();

            if (infoJogadorAlvo != null)
            {
                if (Configuracoes.admJogo.estadoAtual.name == "Atacando" || Configuracoes.admJogo.estadoAtual.name == "Usando Efeito")
                {
                    if (infoJogadorAlvo.jogador == Configuracoes.admJogo.jogadorInimigo)
                    {
                        if (Configuracoes.admJogo.estadoAtual.name == "Atacando")
                        {
                            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoVerde);
                            return;
                        }
                        if (Configuracoes.admJogo.estadoAtual.name == "Usando Efeito" && Configuracoes.admJogo.efeitoAtual.alteracaoVida < 0)
                        {
                            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoVerde);
                        }
                        else
                        {
                            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoVermelho);
                        }
                        return;
                    }
                    else
                    {
                        if (Configuracoes.admJogo.estadoAtual.name == "Usando Efeito" && Configuracoes.admJogo.efeitoAtual.alteracaoVida > 0)
                        {
                            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoVerde);
                        }
                        else
                        {
                            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoVermelho);
                        }
                        return;
                    }
                }

            }
            //se acertou algo, mas não é uma carta
            if (carta != null && Configuracoes.admJogo.estadoAtual != null)
            {
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

            if(!Configuracoes.cartaRecemJogada){
                aoPararDeOlharCarta.Raise();
            }
            
        }
        if (cartaAtual.valor != null)
        {
            cartaAtual.valor.gameObject.SetActive(true);
            cartaAtual.valor = null;
        }
        if (Configuracoes.admJogo.estadoAtual.name == "Em Seu Turno" && Configuracoes.admCursor.sobreBotao == false)
        {
            Configuracoes.admCursor.MudarSprite(cursorIdle);
        }
        if (Configuracoes.admJogo.estadoAtual.name == "Atacando" || Configuracoes.admJogo.estadoAtual.name == "Usando Efeito")
        {
            Configuracoes.admCursor.MudarSprite(Configuracoes.admJogo.cursorAlvoCinza);
        }
    }
}
