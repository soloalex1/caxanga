using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmTutorial : MonoBehaviour
{
    public PassoTutorial[] passos;
    public PassoTutorial passoAtual;
    int indicePasso;
    GameEventListener gmListener;
    public VariavelTransform campoInimigo;

    public LogicaInstanciaCarta logicaCartaBaixa;
    public GameEvent jogadorAtivouEfeito;



    private void Start()
    {
        indicePasso = 0;
        passoAtual = passos[indicePasso];
        StartCoroutine(passoAtual.AoIniciar());
    }

    public void ChamarTurnoIA()
    {
        if (Configuracoes.turnoDaIATutorial)
        {
            StartCoroutine(turnoIAOponente());
        }
    }

    IEnumerator turnoIAOponente()
    {
        StartCoroutine(Configuracoes.admJogo.FadeTextoTurno(Configuracoes.admJogo.jogadorInimigo));
        yield return new WaitForSeconds(1.5f);
        switch (passoAtual.name)
        {
            case "Passo 7.1":
                foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasMao)
                {
                    if (c.carta.name == "Lobisomem")
                    {
                        c.transform.Find("Fundo da Carta").gameObject.SetActive(false);
                        Configuracoes.admJogo.jogadorInimigo.BaixarCarta(c.transform, campoInimigo.valor, c);
                        c.logicaAtual = logicaCartaBaixa;
                        Configuracoes.admJogo.pause = true;
                        break;
                    }
                }
                break;
            case "Passo 10.1":
                foreach (InstanciaCarta c in Configuracoes.admJogo.jogadorInimigo.cartasMao)
                {
                    if (c.carta.name == "Atirei o pau no gato")
                    {
                        jogadorAtivouEfeito.cartaQueAtivouEvento = c;
                        Configuracoes.admEfeito.eventoAtivador = jogadorAtivouEfeito;
                        jogadorAtivouEfeito.Raise();
                        Configuracoes.admJogo.pause = true;
                        break;
                    }
                }
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(2);
        Configuracoes.admJogo.jogadorAtual.rodada.turno.IniciarTurno();
        Configuracoes.turnoDaIATutorial = false;
    }

    public void ativouEvento()
    {
        //SE NÃO FOR O ULTIMO TEXTO
        if (passoAtual.indiceTexto != passoAtual.textos.Length - 1)
        {
            if (passoAtual.objetosDestacados.Length > 1)
            {
                passoAtual.objetosDestacadosNaTela[passoAtual.indiceObjDestacado].SetActive(false);
                passoAtual.indiceObjDestacado++;
                Instantiate(passoAtual.objetosDestacados[passoAtual.indiceObjDestacado], this.transform);
                passoAtual.objetosDestacadosNaTela[passoAtual.indiceObjDestacado] = passoAtual.objetosDestacados[passoAtual.indiceObjDestacado];
            }
            passoAtual.indiceTexto++;
            passoAtual.AtualizarTexto();
            return;
        }
        else // SE FOR O ULTIMO TEXTO
        {
            passoAtual.FinalizarPasso();
            indicePasso++;
            if (passos.Length > indicePasso)
            {
                passoAtual = passos[indicePasso];
                StartCoroutine(passoAtual.AoIniciar());
            }
        }
    }
}
