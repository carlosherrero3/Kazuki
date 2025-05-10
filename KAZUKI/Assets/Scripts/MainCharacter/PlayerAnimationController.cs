using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    public float jumpThreshold = 0.1f;
    public float fallThreshold = -0.1f;

    private Vector3 previousPosition;
    private float verticalSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        previousPosition = transform.position;
    }

    void Update()
    {
        // Calcular velocidad vertical para detectar salto o caída
        verticalSpeed = (transform.position.y - previousPosition.y) / Time.deltaTime;

        bool isGrounded = characterController.isGrounded;

        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalSpeed", verticalSpeed);

        if (!isGrounded)
        {
            if (verticalSpeed > jumpThreshold)
                animator.SetTrigger("Jump");
            else if (verticalSpeed < fallThreshold)
                animator.SetTrigger("Fall");
        }

        previousPosition = transform.position;
    }
}
