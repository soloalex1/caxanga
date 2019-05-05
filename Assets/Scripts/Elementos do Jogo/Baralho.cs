using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ElementosJogo/Baralho")]
public class Baralho : ScriptableObject
{
    public List<string> cartasBaralho = new List<string>();
    public SeguradorDeJogador jogador;

    public void Embaralhar()
    {
        for (int i = cartasBaralho.Count - 1; i > 0; i--)
        {
            // Randomize a number between 0 and i (so that the range decreases each time)
            int rnd = Random.Range(0, i);

            // Save the value of the current i, otherwise it'll overright when we swap the values
            string temp = cartasBaralho[i];

            // Swap the new and old values
            cartasBaralho[i] = cartasBaralho[rnd];
            cartasBaralho[rnd] = temp;
        }
    }
}
