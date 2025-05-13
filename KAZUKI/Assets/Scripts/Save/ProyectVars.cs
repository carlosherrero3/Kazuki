using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectVars : Singleton<ProyectVars>
{
    // Variables para instanciar
    public string StringActiveBetweenScenes; 

   public static ProyectVars Instance
    {
        get
        {
            return (ProyectVars)_mInstance;
        }
        set
        {
            _mInstance = value;
        }
    }


    protected ProyectVars() { }
}
