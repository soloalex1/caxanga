using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(menuName = "Ações/Selecionar Alvo Efeito")]
public class SelecionarAlvoEfeito : Acao
{
    public EstadoJogador faseDeControle;
    public override void Executar(float d)
    {
        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {                //logica para afetar o jogador inimigo
                InstanciaCarta carta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                InfoUIJogador infoJogadorAlvo = r.gameObject.GetComponentInParent<InfoUIJogador>();
                if (infoJogadorAlvo != null)
                {
                    Configuracoes.admJogo.jogadorAlvo = infoJogadorAlvo.jogador;
                    Configuracoes.RegistrarEvento(Configuracoes.admJogo.jogadorAlvo.nomeJogador + " foi selecionado para sofrer o efeito", Color.white);
                    Configuracoes.admJogo.ExecutarEfeito(Configuracoes.admJogo.efeitoAtual);
                    Configuracoes.admJogo.DefinirEstado(faseDeControle);
                    return;
                }
                else
                {
                    if (carta != null && Configuracoes.admJogo.efeitoAtual.apenasJogador)
                    {
                        Configuracoes.RegistrarEvento("O efeito desta carta só se aplica a jogadores", Color.white);
                        return;
                    }
                }
                if (carta != null && (Configuracoes.admJogo.jogadorAtual.cartasBaixadas.Contains(carta) || Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(carta)))
                {
                    Configuracoes.admJogo.cartaAlvo = carta;
                    Configuracoes.RegistrarEvento(carta.infoCarta.carta.name + " foi selecionado(a) para SOFRER o efeito", Color.white);
                    Configuracoes.admJogo.ExecutarEfeito(Configuracoes.admJogo.efeitoAtual);
                    Configuracoes.admJogo.DefinirEstado(faseDeControle);
                    return;
                }
            }
            if (Configuracoes.admJogo.cartaAlvo == null && Configuracoes.admJogo.jogadorAlvo == null)
            {
                Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                Configuracoes.admJogo.DefinirEstado(faseDeControle);
                Configuracoes.admJogo.efeitoAtual = null;
            }
        }
    }
}
