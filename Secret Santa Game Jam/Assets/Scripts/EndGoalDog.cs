using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoalDog : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement playerMovementScript;
    private Rigidbody2D playerRB;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerMovementScript = player.GetComponent<PlayerMovement>();
            playerRB = player.GetComponent<Rigidbody2D>();

            playerRB.velocity = new Vector2(0, 0);
            playerMovementScript.DisableAndEnableMovement();

            Debug.Log("Level Completed!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerMovementScript = player.GetComponent<PlayerMovement>();

            playerMovementScript.DisableAndEnableMovement();
        }
    }
}
