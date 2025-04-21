using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    // Este método se llama al pulsar el botón de salir
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego..."); // Mensaje para verificar en el editor
        Application.Quit(); // Cierra la aplicación

        // Nota: Application.Quit no funciona en el editor de Unity.
        // Para probar en el editor, puedes usar:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}