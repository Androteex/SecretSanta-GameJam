using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float speed = 6f;
    private float jumpForce = 8f;
    private bool facingRight = true;

    private bool grounded;

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = new Vector3(horizontal * speed, rb.velocity.y);

        if (!facingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (facingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ground")
        {
            grounded = false;
        }
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
        if (context.performed && grounded && rb.velocity.y == 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (context.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
}
