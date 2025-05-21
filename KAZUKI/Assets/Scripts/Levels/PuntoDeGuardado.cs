using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntoDeGuardado : MonoBehaviour
{
    public GameObject panelGuardado;
    public CameraOrbit cameraOrbitScript; // ← Referencia al script de la cámara

    private bool jugadorCerca = false;

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (panelGuardado != null)
            {
                panelGuardado.SetActive(true);
                Time.timeScale = 0f;

                if (cameraOrbitScript != null)
                    cameraOrbitScript.enabled = false;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;

            if (panelGuardado != null)
                panelGuardado.SetActive(false);

            Time.timeScale = 1f;

            if (cameraOrbitScript != null)
                cameraOrbitScript.enabled = true;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Método para cerrar el panel desde un botón en el UI
    public void CerrarPanel()
    {
        if (panelGuardado != null)
            panelGuardado.SetActive(false);

        Time.timeScale = 1f;

        if (cameraOrbitScript != null)
            cameraOrbitScript.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
