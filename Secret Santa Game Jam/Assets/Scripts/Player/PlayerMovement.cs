using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Settings:")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask jumpableLayer;

    public Transform rayCastPos;
    public int lives = 3;

    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public float horizontalMove;

    void Update()
    {
        if (canMove)
        {
            if (!facingRight && horizontalMove > 0f)
            {
                Flip();
            }
            else if (facingRight && horizontalMove < 0f)
            {
                Flip();
            }
        }

        grounded = groundCheck();
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        }
    }

    public bool groundCheck()
    {
        float raycastDistance = 0.2f; //Distance set for raycast
        Vector3 raycastOriginOffset = new Vector3(0f, -0.2f); //Will otherwise hit itself (charactersize.y / 2 should be proper value, might need a bit more).

        // Cast a ray straight down.
        RaycastHit2D hitDown = Physics2D.Raycast(rayCastPos.position, -Vector2.up, raycastDistance, jumpableLayer);
        Debug.DrawRay(rayCastPos.position, -Vector2.up, Color.red, raycastDistance);

        // If it hits something...
        if (hitDown.collider != null)
        {
            //Enables isGrounded
            return true;
        }
        //Disables if grounded.
        return false;
    }

    public void DisableAndEnableMovement()
    {
        canMove = !canMove;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && grounded && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && rb.velocity.y > 0f && canMove)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMove = context.ReadValue<Vector2>().x;
    }
}
