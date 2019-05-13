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
                    if (!Configuracoes.admJogo.efeitoAtual.apenasCarta)
                    {
                        Configuracoes.admJogo.jogadorAlvo = infoJogadorAlvo.jogador;
                        Configuracoes.RegistrarEvento(Configuracoes.admJogo.jogadorAlvo.nomeJogador + " foi selecionado para sofrer o efeito", Color.white);
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.ExecutarEfeito(Configuracoes.admJogo.efeitoAtual));
                        Configuracoes.admJogo.DefinirEstado(faseDeControle);
                        return;
                    }
                    else
                    {
                        Configuracoes.admJogo.cartaAlvo.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                        Configuracoes.RegistrarEvento("O efeito desta carta só se aplica a cartas", Color.white);
                        return;
                    }
                }
                else
                {
                    if (carta != null && Configuracoes.admJogo.efeitoAtual.apenasJogador)
                    {
                        Configuracoes.RegistrarEvento("O efeito desta carta só se aplica a jogadores", Color.white);
                        Configuracoes.admJogo.cartaAlvo.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                        return;
                    }
                }
                if (carta != null && (Configuracoes.admJogo.jogadorAtual.cartasBaixadas.Contains(carta) || Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(carta)))
                {
                    Configuracoes.admJogo.cartaAlvo = carta;
                    Configuracoes.RegistrarEvento(carta.infoCarta.carta.name + " foi selecionado(a) para SOFRER o efeito", Color.white);
                    Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.ExecutarEfeito(Configuracoes.admJogo.efeitoAtual));
                    Configuracoes.admJogo.DefinirEstado(faseDeControle);
                    return;
                }
            }
            if (Configuracoes.admJogo.cartaAlvo == null && Configuracoes.admJogo.jogadorAlvo == null)
            {

                Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                Configuracoes.admJogo.DefinirEstado(faseDeControle);
                Configuracoes.admJogo.efeitoAtual = null;
            }
        }
    }
}
