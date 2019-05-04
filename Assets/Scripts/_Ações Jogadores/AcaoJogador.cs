using UnityEngine;

[CreateAssetMenu(menuName = "Ações/Ações do Jogador")]
public abstract class AcaoJogador : ScriptableObject
{
    public abstract void Executar(SeguradorDeJogador jogador);
}