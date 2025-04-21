using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // Velocidad de movimiento
    public float jumpForce = 5f; // Fuerza inicial del salto
    public float maxJumpForce = 10f; // Fuerza máxima del salto
    public float jumpChargeRate = 5f; // Velocidad de carga del salto

    private Rigidbody rb;
    private bool isGrounded = true; // Para verificar si el personaje está en el suelo
    private float currentJumpForce; // Fuerza de salto actual
    private bool isChargingJump = false; // Si está cargando el salto

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumpForce = jumpForce;
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized * moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Salto con carga de potencia
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            isChargingJump = true;
            currentJumpForce = jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isChargingJump)
        {
            currentJumpForce += jumpChargeRate * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, jumpForce, maxJumpForce);
        }

        if (Input.GetKeyUp(KeyCode.Space) && isChargingJump)
        {
            Jump();
            isChargingJump = false;
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verificar si el personaje está tocando el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
