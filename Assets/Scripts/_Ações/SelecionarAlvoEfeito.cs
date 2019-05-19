using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
[CreateAssetMenu(menuName = "Ações/Selecionar Alvo Efeito")]
public class SelecionarAlvoEfeito : Acao
{
    public EstadoJogador emSeuTurno;
    public Sprite cursorIdle;
    public override void Executar(float d)
    {
        if (Configuracoes.admJogo.efeitoAtual.apenasJogador == false)
        {
            if (Configuracoes.admJogo.efeitoAtual.alteracaoPoder > 0)
            {
                if (Configuracoes.admJogo.jogadorAtual.cartasBaixadas.Count == 0)
                {
                    Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                    Configuracoes.admJogo.efeitoAtual = null;
                    Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                    Configuracoes.admJogo.DefinirEstado(emSeuTurno);
                    return;
                }
            }
            if (Configuracoes.admJogo.efeitoAtual.alteracaoPoder < 0)
            {
                if (Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Count == 0)
                {
                    Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                    Configuracoes.admJogo.efeitoAtual = null;
                    Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                    Configuracoes.admJogo.DefinirEstado(emSeuTurno);
                    return;
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            List<RaycastResult> resultados = Configuracoes.GetUIObjs();
            foreach (RaycastResult r in resultados)
            {                //logica para afetar o jogador inimigo
                InstanciaCarta carta = r.gameObject.GetComponentInParent<InstanciaCarta>();
                if (carta != null && (Configuracoes.admJogo.jogadorAtual.cartasBaixadas.Contains(carta) || Configuracoes.admJogo.jogadorInimigo.cartasBaixadas.Contains(carta)))
                {
                    if (carta != Configuracoes.admJogo.efeitoAtual.cartaQueInvoca)
                    {
                        Configuracoes.admEfeito.eventoAtivador.cartaQueAtivouEvento.efeito.cartaAlvo = carta;
                        Configuracoes.admJogo.efeitoAtual.cartaAlvo = carta;
                        Configuracoes.admJogo.DefinirEstado(emSeuTurno);
                        return;
                    }
                    else
                    {
                        Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                        Configuracoes.admJogo.efeitoAtual = null;
                        Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                        Configuracoes.admJogo.DefinirEstado(emSeuTurno);
                        return;
                    }
                }
            }
            if (Configuracoes.admJogo.efeitoAtual.cartaAlvo == null)
            {
                Configuracoes.admJogo.efeitoAtual.cartaQueInvoca.gameObject.transform.localScale = new Vector3(0.28f, 0.28f, 1);
                Configuracoes.admJogo.efeitoAtual = null;
                Configuracoes.RegistrarEvento("Desisti de escolher um alvo", Color.white);
                Configuracoes.admJogo.DefinirEstado(emSeuTurno);
            }
        }
    }
}
