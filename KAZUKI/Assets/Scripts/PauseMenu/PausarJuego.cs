using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausarJuego : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject panelPausa;
    public CameraOrbit cameraOrbitScript; // Referencia al script de la cámara

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f;
            if (panelPausa != null)
                panelPausa.SetActive(true);

            if (cameraOrbitScript != null)
                cameraOrbitScript.enabled = false;

            Cursor.lockState = CursorLockMode.None; // <- Desbloquea el cursor
            Cursor.visible = true; // <- Lo hace visible

            Debug.Log("El juego se ha pausado");
        }
        else
        {
            Time.timeScale = 1f;
            if (panelPausa != null)
                panelPausa.SetActive(false);

            if (cameraOrbitScript != null)
                cameraOrbitScript.enabled = true;

            Cursor.lockState = CursorLockMode.Locked; // <- Vuelve a bloquear el cursor
            Cursor.visible = false; // <- Lo oculta

            Debug.Log("El juego se ha reanudado");
        }
    }
}
