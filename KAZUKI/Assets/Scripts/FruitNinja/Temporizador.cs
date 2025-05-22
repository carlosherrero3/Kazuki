using UnityEngine;
using TMPro;

public class Temporizador : MonoBehaviour
{
    [Header("Configuración Básica")]
    public float tiempoInicial = 60f;
    public int frutasObjetivo = 50;

    [Header("Personalización de Texto")]
    public TextMeshProUGUI textoContador;
    public string formatoTexto = "{0}"; // {0} será reemplazado por el tiempo
    public string textoFinal = "fin";

    private float tiempoRestante;
    private bool juegoActivo = true;
    private int frutasCortadas = 0;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
        ActualizarTexto();
    }

    private void Update()
    {
        if (juegoActivo && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;
            ActualizarTexto();

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                ActualizarTexto();
                FinalizarJuego();
            }
        }
    }

    private void ActualizarTexto()
    {
        if (textoContador != null)
        {
            if (tiempoRestante > 0)
            {
                // Muestra el tiempo formateado (sin decimales)
                textoContador.text = string.Format(formatoTexto, Mathf.CeilToInt(tiempoRestante));
            }
            else
            {
                // Muestra el texto final cuando el tiempo se acaba
                textoContador.text = textoFinal;
            }
        }
    }

    public void FrutaCortada()
    {
        frutasCortadas++;
    }

    private void FinalizarJuego()
    {
        juegoActivo = false;
        FindObjectOfType<Spawner>().enabled = false;

        PanelResultados panel = FindObjectOfType<PanelResultados>();
        if (frutasCortadas >= frutasObjetivo)
        {
            panel.MostrarVictoria(frutasCortadas, frutasObjetivo);
        }
        else
        {
            panel.MostrarDerrota(frutasCortadas, frutasObjetivo);
        }
    }

    // Método para reiniciar manualmente si es necesario
    public void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoInicial;
        frutasCortadas = 0;
        juegoActivo = true;
        ActualizarTexto();
    }
}