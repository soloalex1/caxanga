using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variáveis/Variável de Carta")]
public class VariavelCarta : ScriptableObject
{

    public InstanciaCarta valor;

    public void Set(InstanciaCarta v){
        
        valor = v;

    }

}
