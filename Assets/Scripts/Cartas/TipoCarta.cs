using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TipoCarta", menuName = "Cartas/TipoCarta", order = 0)]
public abstract class TipoCarta : ScriptableObject
{
    public string nomeTipo;

    public virtual void Inicializar(ExibirInfoCarta e)
    {
        Elemento t = Configurações.GetAdmRecursos().tipoElemento;
        ExibirInfoPropriedades tipo = e.GetPropriedade(t);
        tipo.texto.text = nomeTipo;
        Debug.Log("Vou definir o valor do elemento " + t.name + " para " + tipo.texto.text);
    }
}