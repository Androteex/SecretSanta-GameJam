using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerMovement movementScript;
    private PickUpBlocks pickupScript;
    private Animator anim;
    private Rigidbody2D rb;

    private float horizontalMove;
    private bool grounded;
    private bool holdingBlock;
    private string currentState;

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        pickupScript = GetComponent<PickUpBlocks>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMove = movementScript.horizontalMove;
        grounded = movementScript.grounded;
        holdingBlock = pickupScript.holdingBlock;

        AnimationHandler();
    }

    private void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;

        anim.Play(newState);

        currentState = newState;
    }

    private void AnimationHandler()
    {
        if (horizontalMove != 0 && grounded)
        {
            if (holdingBlock)
            {
                ChangeAnimationState("PlayerRunHolding");
            }
            else
            {
                ChangeAnimationState("PlayerRun");
            }
        }
        else if (!grounded)
        {
            if (rb.velocity.y > 0)
            {
                ChangeAnimationState("PlayerJump");
            }
            else if ( rb.velocity.y < 0)
            {
                ChangeAnimationState("PlayerFall");
            }
        }
        else
        {
            ChangeAnimationState("PlayerIdle");
        }
    }
}
