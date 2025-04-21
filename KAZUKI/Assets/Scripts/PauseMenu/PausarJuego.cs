using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject panelPausa;

    void Start()
    {
        
    }

    void Update() //Verifica si el usuario presiona la tecla "Esc".
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused; //Cambia el estado a pausa.
        if (isPaused) //Pausa o reanuda el juego.
        {
            Time.timeScale = 0f; //Detiene el tiempo
            if (panelPausa != null)
            {
                panelPausa.SetActive(true);
            }
            Debug.Log("El juego se ha pausado");
        }
        else
        {
            Time.timeScale = 1f; //Reanuda el tiempo.
            if (panelPausa != null)
            {
                panelPausa.SetActive(false);
            }
            Debug.Log("El juego se ha reanudado");
        }
    }
}
