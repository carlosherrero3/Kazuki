using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChangerButton : MonoBehaviour
{
    [Header("Button Settings")]
    public List<Button> buttons = new List<Button>();   // Lista de botones
    public List<string> sceneNames = new List<string>(); // Lista de nombres de escenas

    void Start()
    {
        if (buttons.Count != sceneNames.Count)
        {
            Debug.LogWarning("La cantidad de botones no coincide con la cantidad de escenas.");
            return;
        }

        // Asignar la acción a cada botón
        for (int i = 0; i < buttons.Count; i++)
        {
            int index = i;  // Capturar el índice para la correcta asignación
            if (buttons[i] != null)
            {
                buttons[i].onClick.AddListener(() => ChangeScene(index)); // Asignar el evento
            }
            else
            {
                Debug.LogWarning("Uno de los botones no está asignado en el Inspector.");
            }
        }
    }

    public void ChangeScene(int index)
    {
        if (index >= 0 && index < sceneNames.Count && !string.IsNullOrEmpty(sceneNames[index]))
        {
            SceneManager.LoadScene(sceneNames[index]); // Cambiar a la escena correspondiente
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no está configurado o el índice es incorrecto.");
        }
    }
}