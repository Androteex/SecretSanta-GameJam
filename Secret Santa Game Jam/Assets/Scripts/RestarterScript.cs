using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestarterScript : MonoBehaviour
{
    private GameObject player;
    private PlayerRespawnAndSpawn playerRespawnScript;

    private GameObject block;
    private BlockRespawnAndSpawn blockRespawnScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            playerRespawnScript = player.GetComponent<PlayerRespawnAndSpawn>();

            playerRespawnScript.Respawn();
        }

        if (collision.tag == "PickupBlock")
        {
            block = collision.gameObject;
            blockRespawnScript = block.GetComponent<BlockRespawnAndSpawn>();

            blockRespawnScript.Respawn();
        }
    }
}
