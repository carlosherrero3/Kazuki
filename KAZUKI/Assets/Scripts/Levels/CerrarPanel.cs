using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CerrarPanel : MonoBehaviour
{
    public GameObject panelGuardado;
    public CameraOrbit cameraOrbitScript;

    public void Cerrar()
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
