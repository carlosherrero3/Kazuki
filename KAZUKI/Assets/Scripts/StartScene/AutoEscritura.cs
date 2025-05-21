using System.Collections;
using UnityEngine;
using TMPro;

public class AutoEscrituraConDesvanecimiento : MonoBehaviour
{
    public TMP_Text texto; // Referencia al componente TMP_Text
    public string textoCompleto; // Texto completo que se escribirá
    public float velocidadEscritura = 0.05f; // Velocidad de escritura en segundos por carácter
    public float velocidadDesvanecimiento = 1f; // Velocidad de desvanecimiento (más alto, más rápido)
    public float opacidadMinima = 0.5f; // Opacidad mínima durante el desvanecimiento

    private Coroutine escrituraCoroutine;
    private Coroutine desvanecimientoCoroutine;

    void Start()
    {
        IniciarEscritura(); // Inicia automáticamente la escritura (opcional)
    }

    public void IniciarEscritura()
    {
        // Detener cualquier escritura o desvanecimiento en curso
        if (escrituraCoroutine != null)
            StopCoroutine(escrituraCoroutine);

        if (desvanecimientoCoroutine != null)
            StopCoroutine(desvanecimientoCoroutine);

        escrituraCoroutine = StartCoroutine(EscribirTexto());
    }

    private IEnumerator EscribirTexto()
    {
        texto.text = ""; // Limpiar el texto antes de comenzar
        texto.alpha = 1f; // Asegurar que el texto es completamente visible

        foreach (char letra in textoCompleto.ToCharArray())
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        // Iniciar desvanecimiento al finalizar la escritura
        desvanecimientoCoroutine = StartCoroutine(DesvanecerTexto());
    }

    private IEnumerator DesvanecerTexto()
    {
        float alpha = 1f;
        bool disminuyendo = true;

        while (true)
        {
            if (disminuyendo)
            {
                alpha -= Time.deltaTime * velocidadDesvanecimiento;
                if (alpha <= opacidadMinima)
                {
                    alpha = opacidadMinima;
                    disminuyendo = false;
                }
            }
            else
            {
                alpha += Time.deltaTime * velocidadDesvanecimiento;
                if (alpha >= 1f)
                {
                    alpha = 1f;
                    disminuyendo = true;
                }
            }

            texto.alpha = alpha;
            yield return null;
        }
    }

    public void DetenerEscritura()
    {
        // Detener escritura y desvanecimiento
        if (escrituraCoroutine != null)
            StopCoroutine(escrituraCoroutine);

        if (desvanecimientoCoroutine != null)
            StopCoroutine(desvanecimientoCoroutine);

        texto.text = textoCompleto; // Completar el texto inmediatamente
        texto.alpha = 1f; // Asegurar que el texto quede completamente visible
    }
}