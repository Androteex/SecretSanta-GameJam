using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRespawnAndSpawn : MonoBehaviour
{
    private Vector3 startPos;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        Debug.Log(startPos);
    }

    public void Respawn()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector3(startPos.x, startPos.y + 1, 0);
    }
}
