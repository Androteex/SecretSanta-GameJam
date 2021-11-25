using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrapple : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private Vector2 mousePoint;
    private Vector2 projectedMousePoint;

    private PlayerMovement movementScript;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.FindObjectOfType<Camera>();
        distanceJoint.enabled = false;
        movementScript = GetComponent<PlayerMovement>();
    }
        
    // Update is called once per frame
    void Update()
    {
        if (distanceJoint.enabled)
        {
            lineRenderer.SetPosition(1, transform.position);
        }

        projectedMousePoint = mainCamera.ScreenToWorldPoint(mousePoint);
    }

    public void Grapple(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Grapple Performed!");
            lineRenderer.SetPosition(1, transform.position);
            lineRenderer.SetPosition(0, projectedMousePoint);
            distanceJoint.connectedAnchor = projectedMousePoint;
            distanceJoint.enabled = true;
            lineRenderer.enabled = true;

            movementScript.enabled = false;
        }
        else if (context.canceled)
        {
            distanceJoint.enabled = false;
            lineRenderer.enabled = false;

            movementScript.enabled = true;
        }
    }

    public void MousePoint(InputAction.CallbackContext context)
    {
        mousePoint = context.ReadValue<Vector2>();
    }
}
