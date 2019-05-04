using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Console/Hook")]
public class ConsoleHook : ScriptableObject
{
    [System.NonSerialized]
    public AdmConsole admConsole;
    public void RegistrarEvento(string s, Color color)
    {
        admConsole.RegistrarEvento(s, color);
    }
}
