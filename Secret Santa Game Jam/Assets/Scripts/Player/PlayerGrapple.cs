using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapple : MonoBehaviour
{
    // Variables shown in inspector
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float maxGrappleDistance;
    [SerializeField] private float minMoveDistance;
    [SerializeField] private LayerMask platformLayer;

    // Private variables
    private Camera mainCamera;
    private Vector2 mousePoint;
    private Vector2 projectedMousePoint;

    // Static Variables
    public static Vector2 rayDir;
    public static bool grappling;

    // MIGHT NEED
    // private PlayerMovement movementScript;

    void Start()
    {
        mainCamera = Camera.FindObjectOfType<Camera>();
        // movementScript = GetComponent<PlayerMovement>();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (grappling)
        {
            lineRenderer.SetPosition(1, transform.position);
        }

        projectedMousePoint = mainCamera.ScreenToWorldPoint(mousePoint);
    }

    public void Grapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rayDir = new Vector2(projectedMousePoint.x - transform.position.x, projectedMousePoint.y - transform.position.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDir, maxGrappleDistance, platformLayer);
            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, transform.position);
                lineRenderer.SetPosition(0, hit.point);
                lineRenderer.enabled = true;
                grappling = true;

                if (Vector2.Distance(transform.position, hit.point) <= minMoveDistance)
                {
                    lineRenderer.enabled = false;
                    grappling = false;
                }
            }
        }
        else if (context.canceled)
        {
            lineRenderer.enabled = false;
            grappling = false;
        }
    }

    public void MousePoint(InputAction.CallbackContext context)
    {
        mousePoint = context.ReadValue<Vector2>();
    }
}
