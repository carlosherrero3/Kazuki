using UnityEngine;
using TMPro;
using System.Collections;

public class MensajeInicial: MonoBehaviour
{
    [Header("Configuración del Mensaje")]
    [SerializeField] private GameObject panelMensaje;
    [SerializeField] private TextMeshProUGUI textoMensaje;
    [TextArea(3, 10)][SerializeField] private string frase = "Escribe aquí tu mensaje inicial";
    [SerializeField] private float tiempoPorLetra = 0.05f;
    [SerializeField] private float tiempoVisibleDespues = 2f;
    [SerializeField] private float velocidadFade = 1f;
    [SerializeField] private float fadeInDuration = 1f; // Nuevo: Duración del fade in

    [Header("Control del Jugador")]
    [SerializeField] private MonoBehaviour[] scriptsJugador;
    [SerializeField] private AudioClip sonidoEscritura;
    [SerializeField] private bool bloquearMovimientoCamara = true;

    private CanvasGroup canvasGroup;
    private AudioSource audioSource;
    private bool bloqueoActivo = false;

    private void Start()
    {
        InitializeComponents();
        StartCoroutine(SequenciaCompleta());
    }

    private void InitializeComponents()
    {
        // Configuración inicial del panel con fade in
        if (panelMensaje != null)
        {
            canvasGroup = panelMensaje.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = panelMensaje.AddComponent<CanvasGroup>();
            }
            canvasGroup.alpha = 0f; // Empieza invisible
            panelMensaje.SetActive(true);
        }

        // Configurar audio
        if (sonidoEscritura != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sonidoEscritura;
            audioSource.loop = true;
        }

        // Limpiar texto inicial
        if (textoMensaje != null)
        {
            textoMensaje.text = "";
        }
    }

    private IEnumerator SequenciaCompleta()
    {
        // 1. Fade In del panel
        yield return StartCoroutine(EfectoFade(0f, 1f, fadeInDuration));

        // 2. Bloquear controles (después del fade in)
        DesactivarControlesJugador(true);

        // 3. Efecto de escritura
        yield return StartCoroutine(EscribirTexto());

        // 4. Tiempo con mensaje completo
        yield return new WaitForSeconds(tiempoVisibleDespues);

        // 5. Fade Out
        yield return StartCoroutine(EfectoFade(1f, 0f, velocidadFade));

        // 6. Finalizar
        panelMensaje.SetActive(false);
        DesactivarControlesJugador(false);
    }

    private IEnumerator EfectoFade(float inicio, float fin, float duracion)
    {
        float tiempoTranscurrido = 0f;
        while (tiempoTranscurrido < duracion)
        {
            canvasGroup.alpha = Mathf.Lerp(inicio, fin, tiempoTranscurrido / duracion);
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = fin;
    }

    private IEnumerator EscribirTexto()
    {
        // Iniciar sonido (si existe)
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Escribir letra por letra
        for (int i = 0; i <= frase.Length; i++)
        {
            textoMensaje.text = frase.Substring(0, i);
            yield return new WaitForSeconds(tiempoPorLetra);
        }

        // Detener sonido
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    private void DesactivarControlesJugador(bool desactivar)
    {
        bloqueoActivo = desactivar;

        foreach (var script in scriptsJugador)
        {
            if (script != null)
            {
                script.enabled = !desactivar;
            }
        }

        Cursor.lockState = desactivar ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = desactivar;

        // Bloqueo adicional de cámara
        if (bloquearMovimientoCamara)
        {
            var mouseLook = Camera.main.GetComponent<MonoBehaviour>();
            if (mouseLook != null)
            {
                var sensitivity = mouseLook.GetType().GetProperty("sensitivity");
                if (sensitivity != null)
                {
                    sensitivity.SetValue(mouseLook, desactivar ? 0f : 2f); // 2f es sensibilidad por defecto
                }
            }
        }
    }

    public void SaltarEscritura()
    {
        StopAllCoroutines();
        textoMensaje.text = frase;
        StartCoroutine(DesvanecerDespuesDeSaltar());
    }

    private IEnumerator DesvanecerDespuesDeSaltar()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        yield return new WaitForSeconds(tiempoVisibleDespues);

        yield return StartCoroutine(EfectoFade(canvasGroup.alpha, 0f, velocidadFade));

        panelMensaje.SetActive(false);
        DesactivarControlesJugador(false);
    }

    // Bloqueo adicional de movimiento de cámara en Update
    private void Update()
    {
        if (bloqueoActivo && bloquearMovimientoCamara)
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                var mouseLook = Camera.main.GetComponent<MonoBehaviour>();
                if (mouseLook != null)
                {
                    var xRotation = mouseLook.GetType().GetProperty("xRotation");
                    var yRotation = mouseLook.GetType().GetProperty("yRotation");
                    if (xRotation != null) xRotation.SetValue(mouseLook, 0f);
                    if (yRotation != null) yRotation.SetValue(mouseLook, 0f);
                }
            }
        }
    }
}