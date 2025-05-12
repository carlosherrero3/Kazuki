using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeGuardado : MonoBehaviour
{
    [Tooltip("El collider que activará el cambio de escena.")]
    public Collider targetCollider;

    [Tooltip("Nombre de la escena a cargar al colisionar con el targetCollider.")]
    public string targetScene;

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en contacto es el jugador
        if (other.CompareTag("Player") && other == targetCollider)
        {
            // Cambia a la escena especificada
            SceneManager.LoadScene(targetScene);
        }
    }
}
