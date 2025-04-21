using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkText : MonoBehaviour
{
    public TextMeshProUGUI textToBlink; // El texto que quieres que parpadee
    public float blinkSpeed = 1.0f;     // Velocidad de parpadeo (más alto = más lento)

    private float alpha = 1.0f;         // Nivel de opacidad actual (0 a 1)
    private bool fadingOut = true;     // Indica si el texto está desapareciendo

    void Update()
    {
        if (textToBlink == null)
        {
            Debug.LogError("No se ha asignado un TextMeshProUGUI al script.");
            return;
        }

        // Ajustar opacidad según el estado de desvanecimiento
        if (fadingOut)
        {
            alpha -= Time.deltaTime / blinkSpeed;
            if (alpha <= 0.0f)
            {
                alpha = 0.0f;
                fadingOut = false;
            }
        }
        else
        {
            alpha += Time.deltaTime / blinkSpeed;
            if (alpha >= 1.0f)
            {
                alpha = 1.0f;
                fadingOut = true;
            }
        }

        // Aplicar opacidad al texto
        Color color = textToBlink.color;
        color.a = alpha;
        textToBlink.color = color;
    }
}