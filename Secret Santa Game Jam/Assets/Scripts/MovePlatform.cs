using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private Vector3 startPos = new Vector3(24.5f, -3.5f, 0f);

    private void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        transform.position = startPos;
    }

    public void MovePlatformToPoint()
    {
        transform.position = new Vector3(transform.position.x - 4, transform.position.y, transform.position.z);
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
