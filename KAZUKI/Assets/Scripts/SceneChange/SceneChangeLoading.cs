using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeLoading : MonoBehaviour
{
    // Variable para configurar el nombre de la escena desde el editor o de forma din�mica
    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(sceneName))
            {
                LevelLoader.LoadLevel(sceneName); // Carga la escena configurada
            }
            else
            {
                Debug.LogWarning("El nombre de la escena no est� configurado.");
            }
        }
    }
}
