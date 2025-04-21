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

    public Toggle fullscreenToggle;

    //
    public TMP_Dropdown resolucionesDropDown;
    Resolution[] resoluciones;
    //

    void Start()
    {
  

        //
        RevisarResolucion();
        //
        // Asegúrate de que el Toggle refleje el estado actual
        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            fullscreenToggle.onValueChanged.AddListener(ToggleFullscreen);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
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

}
