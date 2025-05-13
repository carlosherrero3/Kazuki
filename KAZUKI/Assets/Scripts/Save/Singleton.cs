using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Se usa para pasar datos de una escena a otra
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static Singleton<T> _mInstance;
    public static Singleton<T> Instance
    {
        get
        {
            if (!_mInstance)
            {
                T[] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];
                if (managers.Length != 0)
                {
                    if (managers.Length == 1)
                    {
                        _mInstance = managers[0];
                        _mInstance.gameObject.name = typeof(T).Name;
                        return _mInstance;
                    }
                    else
                    {
                        Debug.LogError("You have more than one " + typeof(T).Name + " in the scene. You only need 1, it's a singleton!");
                        foreach (T manager in managers)
                        {
                            Destroy(manager.gameObject);
                        }
                    }
                }
                GameObject g0 = new GameObject(typeof(T).Name, typeof(T));
                _mInstance = g0.GetComponent<T>();
                DontDestroyOnLoad(g0);
            }
            return _mInstance;
        }
        set
        {
            _mInstance = value as T;
        }
    }
}