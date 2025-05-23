using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Historia : MonoBehaviour
{
    public TextMeshProUGUI historiaTexto;
    [TextArea(3, 10)]
    public string[] parrafos;
    public float velocidadEscritura = 0.05f; // Tiempo entre letras

    private int indice = 0;
    private bool escribiendo = false;
    private bool finDelTexto = false; // NUEVA VARIABLE

    void Start()
    {
        StartCoroutine(EscribirTexto(parrafos[indice]));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (escribiendo)
            {
                // Si se hace clic durante la escritura, mostrar el texto completo
                StopAllCoroutines();
                historiaTexto.text = parrafos[indice];
                escribiendo = false;
            }
            else
            {
                if (finDelTexto)
                {
                    // CAMBIO DE ESCENA AQUÍ
                    SceneManager.LoadScene("NombreDeTuEscena"); // Reemplaza con el nombre correcto
                    return;
                }

                indice++;
                if (indice < parrafos.Length)
                {
                    StartCoroutine(EscribirTexto(parrafos[indice]));
                }
                else
                {
                    historiaTexto.text = "";
                    finDelTexto = true; // Ya no hay más texto, próxima pulsación cambia de escena
                }
            }
        }
    }

    IEnumerator EscribirTexto(string texto)
    {
        escribiendo = true;
        historiaTexto.text = "";
        foreach (char letra in texto.ToCharArray())
        {
            historiaTexto.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }
        escribiendo = false;
    }
}
