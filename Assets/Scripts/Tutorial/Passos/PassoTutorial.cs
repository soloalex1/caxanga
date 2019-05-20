using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tutorial/Passo")]
public class PassoTutorial : ScriptableObject
{
    public List<VariavelTransform> posicoesPopUps;
    public GameObject popUp;

    public List<string> textos;

    public GameEvent eventoProximoPasso;

}
