using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Configuracoes
{
    private static AdmRecursos _admRecursos;
    public static AdmRecursos GetAdmRecursos()
    {
        if (_admRecursos == null)
        {
            _admRecursos = Resources.Load("AdmRecursos") as AdmRecursos;
        }
        return _admRecursos;
    }
}
