using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelResultados : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject panelVictoria;    // Panel con mensaje (sin botones)
    public GameObject panelDerrota;    // Panel con botones

    [Header("Textos")]
    public TextMeshProUGUI textoResultado;
    public TextMeshProUGUI textoFrutas;

    [Header("Botones Derrota")]
    public Button botonReiniciar;
    public Button botonSalir;

    [Header("Escenas")]
    public string escenaMenu = "Menu Inicio";
    public string escenaNivel1 = "Nivel 1";

    private void Start()
    {
        botonReiniciar.onClick.AddListener(ReiniciarJuego);
        botonSalir.onClick.AddListener(SalirAMenu);

        panelVictoria.SetActive(false);
        panelDerrota.SetActive(false);
    }

    public void MostrarVictoria(int frutasCortadas, int frutasObjetivo)
    {
        textoResultado.text = "Enhorabuena, has superado el reto";
        textoFrutas.text = $" {frutasCortadas} / ";
        panelVictoria.SetActive(true);

        Invoke(nameof(CambiarANivel1), 2f);
    }

    public void MostrarDerrota(int frutasCortadas, int frutasObjetivo)
    {
        textoResultado.text = "Vaya... fallaste";
        textoFrutas.text = $"{frutasCortadas}/{frutasObjetivo} ";
        panelDerrota.SetActive(true);
    }

    private void CambiarANivel1()
    {
        SceneManager.LoadScene(escenaNivel1);
    }

    private void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void SalirAMenu()
    {
        SceneManager.LoadScene(escenaMenu);
    }
}