using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AdmRecursos")]
public class AdmRecursos : ScriptableObject
{
    //AdmRecursos vai justamente administrar os recursos do jogo
    public Elemento tipoElemento;//esses elementos são UI. Esse Elemento é um scriptale object. Os tipos que temos são Imagem, Texto e Inteiro
    public Carta[] todasAsCartas;//todas as cartas que estão em jogo

    Dictionary<string, Carta> dicionarioCartas = new Dictionary<string, Carta>();//um dicionário para referenciar as cartas mais facilmente
    public void AoIniciar()
    {
        dicionarioCartas.Clear();//limpa o dicionário
        for (int i = 0; i < todasAsCartas.Length; i++)
        {
            //adiciona todas as cartas, definindo sua key como sendo o nome da carta
            dicionarioCartas.Add(todasAsCartas[i].name, todasAsCartas[i]);
        }
    }
    Carta obterCarta(string id)//retorna uma carta pelo id dela, buscando no dicionário
    {
        Carta resultado = null;
        dicionarioCartas.TryGetValue(id, out resultado);//busca a carta (classe) no dicionário
        return resultado;//se ela não existir, retorna nulo
    }
    public Carta obterInstanciaCarta(string id)//retorna uma carta instanciada (objeto) 
    {
        Carta cartaOriginal = obterCarta(id);//busca a classe da carta... ex: boitatá
        if (cartaOriginal == null)//se não existir, retorna nulo
            return null;
        Carta novaInstancia = Instantiate(cartaOriginal);//se existir, instancia ela
        novaInstancia.name = cartaOriginal.name;//garantir q o nome vai ser o mesmo, em caso de querer usar outros dicionários
        return novaInstancia;//retorna a carta
    }

}