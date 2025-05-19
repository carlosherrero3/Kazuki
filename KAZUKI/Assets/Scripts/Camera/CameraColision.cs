using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraColision : MonoBehaviour
{
    public Transform target; // Objeto que la cámara sigue (generalmente el jugador)
    public float smoothSpeed = 0.1f; // Suavidad del movimiento
    public float cameraDistance = 5f; // Distancia deseada de la cámara al objetivo

    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.position - target.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + cameraOffset.normalized * cameraDistance;

        // Realiza un raycast desde el objetivo hacia la cámara
        RaycastHit hit;
        if (Physics.Raycast(target.position, -cameraOffset.normalized, out hit, cameraDistance))
        {
            // Si hay colisión, ajusta la cámara al punto de impacto
            desiredPosition = hit.point + cameraOffset.normalized * 0.2f;
        }

        // Mueve suavemente la cámara a la posición deseada
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.LookAt(target);
    }
}
