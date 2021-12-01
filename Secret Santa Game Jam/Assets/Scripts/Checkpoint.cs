using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject player;
    private PlayerRespawnAndSpawn respawnScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            respawnScript = player.GetComponent<PlayerRespawnAndSpawn>();

            respawnScript.checkPoint = transform;
        }
    }
}
