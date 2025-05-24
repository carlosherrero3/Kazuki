using UnityEngine;

public class ObjetoInteractuable : MonoBehaviour
{
    [Header("Configuraci�n")]
    public GameObject objetoParaRecoger; // El objeto que se recoger�
    public Transform manoJugador; // El GameObject vac�o en la mano del jugador
    public Sprite spriteTeclaE; // El sprite de la tecla E a mostrar
    public float distanciaInteraccion = 2f; // Distancia a la que se puede interactuar

    [Header("UI")]
    public GameObject uiInteraccion; // Objeto UI que muestra la tecla E (debe tener un componente Image)

    private bool jugadorEnRango = false;
    private bool objetoRecogido = false;
    private SpriteRenderer spriteRendererOriginal;
    private Vector3 posicionOriginal;
    private Quaternion rotacionOriginal;
    private Transform padreOriginal;

    private void Start()
    {
        // Configuraci�n inicial
        if (uiInteraccion != null)
        {
            uiInteraccion.SetActive(false);
        }

        // Guardar propiedades originales del objeto
        if (objetoParaRecoger != null)
        {
            spriteRendererOriginal = objetoParaRecoger.GetComponent<SpriteRenderer>();
            posicionOriginal = objetoParaRecoger.transform.position;
            rotacionOriginal = objetoParaRecoger.transform.rotation;
            padreOriginal = objetoParaRecoger.transform.parent;
        }
    }

    private void Update()
    {
        if (jugadorEnRango && !objetoRecogido)
        {
            // Mostrar UI de interacci�n
            if (uiInteraccion != null)
            {
                uiInteraccion.SetActive(true);
            }

            // Verificar si se presiona la tecla E
            if (Input.GetKeyDown(KeyCode.E))
            {
                RecogerObjeto();
            }
        }
        else
        {
            // Ocultar UI de interacci�n
            if (uiInteraccion != null)
            {
                uiInteraccion.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !objetoRecogido)
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnRango = false;
        }
    }

    private void RecogerObjeto()
    {
        if (objetoParaRecoger != null && manoJugador != null)
        {
            // Mover el objeto a la mano del jugador
            objetoParaRecoger.transform.SetParent(manoJugador);
            objetoParaRecoger.transform.localPosition = Vector3.zero;
            objetoParaRecoger.transform.localRotation = Quaternion.identity;

            objetoRecogido = true;
            jugadorEnRango = false;

            Debug.Log("Objeto recogido: " + objetoParaRecoger.name);
        }
    }

    // M�todo para soltar el objeto (opcional)
    public void SoltarObjeto()
    {
        if (objetoRecogido && objetoParaRecoger != null)
        {
            // Devolver el objeto a su posici�n original
            objetoParaRecoger.transform.SetParent(padreOriginal);
            objetoParaRecoger.transform.position = posicionOriginal;
            objetoParaRecoger.transform.rotation = rotacionOriginal;

            objetoRecogido = false;
        }
    }
}