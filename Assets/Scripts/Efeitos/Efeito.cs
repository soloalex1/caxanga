using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cartas/Efeito/Novo Efeito")]
public class Efeito : ScriptableObject
{
    public Ativacao ativacao;
    public ModoDeExecucao modoDeExecucao;
    public TipoEfeito tipoEfeito;
    public GameEvent eventoAtivador;
    public CondicaoAtivacao condicaoAtivacao;
    public SeguradorDeJogador jogadorQueInvoca;
    public SeguradorDeJogador jogadorAlvo;
    public InstanciaCarta cartaQueInvoca;
    public InstanciaCarta cartaAlvo;
    public int alteracaoPoder;
    public int alteracaoVida;
    public int alteracaoMagia;
    public bool apenasJogador;
    public bool apenasCarta;
    public bool podeUsarEmSi;
    public bool afetaTodasCartas;

}
