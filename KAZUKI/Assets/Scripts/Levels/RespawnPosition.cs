using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPosition : MonoBehaviour
{

    public Vector3 respawnPosition; // Coordenadas donde reaparecerá el personaje

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra en contacto es el jugador
        if (other.CompareTag("Player"))
        {
            // Mueve al jugador a las coordenadas de respawn
            other.transform.position = respawnPosition;

            // Opcional: Reinicia la velocidad del Rigidbody, si tiene uno
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}