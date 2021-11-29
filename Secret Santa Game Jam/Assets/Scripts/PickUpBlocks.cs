using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpBlocks : MonoBehaviour
{
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private float maxDistance;

    private bool holdingBlock = false;
    private GameObject block;

    public void PickUpBlock(InputAction.CallbackContext context)
    {
        if (context.performed && !holdingBlock)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.right, maxDistance, boxLayer);

            if (hit.collider != null)
            {
                Debug.DrawLine(transform.position, hit.transform.position, Color.green, 1f);
                block = hit.transform.gameObject;
                block.transform.parent = this.transform;
                block.GetComponent<Rigidbody2D>().isKinematic = true;
                block.GetComponent<BoxCollider2D>().isTrigger = true;
                block.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.y);
            }
        }
    }
}
