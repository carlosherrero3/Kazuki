using UnityEngine;

public class Temporizador : MonoBehaviour
{
    [Header("Configuración")]
    public float tiempoInicial = 60f;
    public int frutasObjetivo = 50;

    private float tiempoRestante;
    private bool juegoActivo = true;
    private int frutasCortadas = 0;

    private void Start()
    {
        tiempoRestante = tiempoInicial;
    }

    private void Update()
    {
        if (juegoActivo && tiempoRestante > 0)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0)
            {
                tiempoRestante = 0;
                FinalizarJuego();
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
        FindObjectOfType<Spawner>().enabled = false; // Detiene el spawner

        PanelResultados panel = FindObjectOfType<PanelResultados>();
        if (frutasCortadas >= frutasObjetivo)
        {
            panel.MostrarVictoria(frutasCortadas);
        }
        else
        {
            panel.MostrarDerrota(frutasCortadas, frutasObjetivo);
        }
    }
}