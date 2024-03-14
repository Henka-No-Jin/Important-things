using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float groundDist;

    public LayerMask TerrainLayer;
    private CharacterController characterController;
    private SpriteRenderer spriteRenderer;
    private Vector3 lastMoveDirection = Vector3.forward;
    private Animator animator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 castpost = transform.position;
        castpost.y += 1;

        if (Physics.Raycast(castpost, -transform.up, out hit, Mathf.Infinity, TerrainLayer))
        {
            float targetY = hit.point.y + groundDist;
            characterController.Move(Vector3.up * (targetY - transform.position.y));
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        if (moveDirection.magnitude > 0.1f)
        {
            // Set isRunning parameter based on movement input
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);

            // Flip the sprite based on the horizontal input
            spriteRenderer.flipX = (horizontal < 0);
            animator.SetBool("isRunningLeft", spriteRenderer.flipX);

            // Move the character
            characterController.Move(moveDirection * speed * Time.deltaTime);
        }
        else
        {
            // Set isIdle parameter if there is no movement
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
            animator.SetBool("isIdleLeft", spriteRenderer.flipX);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {

        }
    }

}
