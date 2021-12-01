using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDoor : MonoBehaviour
{
    public OpenDoor doorScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PickupBlock")
        {
            doorScript.OpenThisDoor();
        }
    }
}
