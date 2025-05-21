using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
//
using TMPro;
//

public class LogicaScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public Toggle toggle;

    //
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;
    //

    void Start()
    {

        // Asegúrate de que el Toggle refleje el estado actual
        if (Screen.fullScreen)
        {
            toggle.isOn = true;
        }
        else
        {
            toggle.isOn = false;
        }

        RevisarResolucion();
        SetHighest16By9Resolution();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivatePantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    //
    public void RevisarResolucion() 
    {
        resoluciones = Screen.resolutions;
        resolucionesDropDown.ClearOptions();
        List<string> opciones = new List<string>();
        int resolucionActual = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            string opcion=resoluciones[i].width + "x" + resoluciones[i].height;
            opciones.Add(opcion);

            if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
                resoluciones[i].height == Screen.currentResolution.height) 
            {
                resolucionActual = i;
            }
        }

        resolucionesDropDown.AddOptions(opciones);
        resolucionesDropDown.value = resolucionActual;
        resolucionesDropDown.RefreshShownValue();
    }

    public void CambiarResolucion(int indiceResolucion) 
    {
        Resolution resolucion = resoluciones[indiceResolucion];
        Screen.SetResolution(resolucion.width,resolucion.height,Screen.fullScreen);
    }
    //
    private void SetHighest16By9Resolution()
    {
        Resolution bestResolution = Screen.currentResolution; // Iniciar con la resolución actual
        int bestResolutionIndex = 0;

        for (int i = 0; i < resoluciones.Length; i++)
        {
            float aspectRatio = (float)resoluciones[i].width / resoluciones[i].height;

            // Verificar si la resolución es 16:9 y mayor que la mejor actual
            if (Mathf.Approximately(aspectRatio, 16f / 9f) &&
                resoluciones[i].width > bestResolution.width)
            {
                bestResolution = resoluciones[i];
                bestResolutionIndex = i;
            }
        }

        // Establecer la resolución más alta en 16:9
        Screen.SetResolution(bestResolution.width, bestResolution.height, Screen.fullScreen);

        // Actualizar el Dropdown al índice de la mejor resolución
        resolucionesDropDown.value = bestResolutionIndex;
        resolucionesDropDown.RefreshShownValue();
    }

}
