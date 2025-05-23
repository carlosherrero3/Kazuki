using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTriggerLook : MonoBehaviour
{
    public GameObject spriteObject;  // El objeto que contiene el sprite
    public Transform player;         // Referencia al jugador

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spriteObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (spriteObject.activeSelf && player != null)
        {
            Vector3 direction = player.position - spriteObject.transform.position;
            direction.y = 0f; // Ignorar la rotación en el eje Y para que no se incline hacia arriba o abajo
            if (direction != Vector3.zero)
            {
                spriteObject.transform.rotation = Quaternion.LookRotation(-direction);
            }
        }
    }
}
