using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmTutorial : MonoBehaviour
{
    public AudioClip somBotaoPressed;
    public PassoTutorial[] passos;
    public PassoTutorial passoAtual;
    int indicePasso;
    GameEventListener gmListener;
    public VariavelTransform campoInimigo;

    public LogicaInstanciaCarta logicaCartaBaixa;
    public GameEvent jogadorAtivouEfeito;

    private void Start()
    {
        Configuracoes.admTutorial = this;
        indicePasso = 0;
        passoAtual = passos[indicePasso];
        StartCoroutine(passoAtual.AoIniciar());
        GetComponent<AudioSource>().volume = Configuracoes.volumeSFX;
        gmListener = GetComponent<GameEventListener>();
    }

    public void ChamarTurnoIA()
    {
        if (Configuracoes.turnoDaIATutorial)
        {
            StartCoroutine(turnoIAOponente());
        }
    }

    public void TocarSomBotao()
    {
        if (GetComponent<AudioSource>().isPlaying)
            GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().clip = somBotaoPressed;
        GetComponent<AudioSource>().Play();
    }
    IEnumerator turnoIAOponente()
    {
        StartCoroutine(Configuracoes.admJogo.FadeTextoTurno(Configuracoes.admJogo.jogadorInimigo));
        yield return new WaitForSeconds(2f);
        // Debug.Log(passoAtual.name);
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
                        Configuracoes.admJogo.StartCoroutine(Configuracoes.admJogo.DestacarCartaBaixada(c));
                        Configuracoes.admJogo.pause = true;
                        break;
                    }
                }
                break;
            case "Passo 11":
                Configuracoes.admJogo.jogadorInimigo.rodada.turno.FinalizarTurno();
                Configuracoes.admJogo.jogadorInimigo.rodada.PassarRodada();
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(2.5f);
        Configuracoes.admJogo.jogadorAtual.rodada.turno.IniciarTurno();
        yield return new WaitForSeconds(1);
        Configuracoes.turnoDaIATutorial = false;
    }
    private void Update()
    {
        if (passoAtual.jogadorInterage == false)
        {
            Configuracoes.admJogo.pause = true;
        }
    }
    public void ativouEvento()
    {
        if (Configuracoes.eventoDisparado == passoAtual.eventoFinalizador)
        {
            //SE NÃO FOR O ULTIMO TEXTO
            if (passoAtual.indiceTexto < passoAtual.textos.Length - 1)
            {
                passoAtual.ProximoTexto();
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
                else
                {
                    Configuracoes.admCena.CarregarCena("Tela Inicial");
                }
            }
        }
        else
        {
            if (gmListener.gameEvents.Contains(Configuracoes.eventoDisparado))
            {
                Debug.Log("Vc fez o evento errado hein");
            }
        }
    }
}
