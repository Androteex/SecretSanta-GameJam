using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PickUpBlocks : MonoBehaviour
{
    [SerializeField] private LayerMask boxLayer;
    [SerializeField] private float maxDistance;

    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite holdingSprite;

    private PlayerMovement movementScript;
    private float force = 15f;
    private Vector2 mousePoint;
    private Vector2 projectedMousePoint;
    private Vector2 forceDir;
    private RaycastHit2D hit;
    private GameObject block;
    private GameObject mouseTracker;
    private SpriteRenderer spriteRenderer;

    public bool holdingBlock = false;

    public GameObject pointPrefab;
    private GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;

    private void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = normalSprite;
    }
    private void Update()
    {
        projectedMousePoint = Camera.main.ScreenToWorldPoint(mousePoint);

        forceDir = (projectedMousePoint - ((Vector2)transform.position));

        if (holdingBlock)
        {
            mouseTracker = block.transform.Find("MouseTracker").gameObject;
            mouseTracker.transform.right = forceDir.normalized;

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints);
            }
        }
    }

    Vector2 PointPosition(float t)
    {
        Vector2 position = (Vector2)block.transform.position + ((Vector2)mouseTracker.transform.right * force * t) + 0.5f * (Physics2D.gravity * 2) * (t * t);
        return position;
    }

    public void PickUpBlock(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!holdingBlock)
            {
                if (movementScript.facingRight)
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.right, maxDistance, boxLayer);
                }
                else
                {
                    hit = Physics2D.Raycast(transform.position, Vector3.left, maxDistance, boxLayer);
                }

                if (hit.collider != null && movementScript.facingRight)
                {
                    block = hit.transform.gameObject;
                    block.transform.parent = this.transform;
                    block.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    block.GetComponent<Rigidbody2D>().isKinematic = true;
                    block.GetComponent<BoxCollider2D>().isTrigger = true;
                    block.transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.45f, transform.position.y);
                    holdingBlock = true;

                    spriteRenderer.sprite = holdingSprite;

                    points = new GameObject[numberOfPoints];
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i] = Instantiate(pointPrefab, block.transform.position, Quaternion.identity);
                    }
                }
                else if (hit.collider != null && !movementScript.facingRight)
                {
                    block = hit.transform.gameObject;
                    block.transform.parent = this.transform;
                    block.GetComponent<Rigidbody2D>().isKinematic = true;
                    block.GetComponent<BoxCollider2D>().isTrigger = true;
                    block.transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.45f, transform.position.y);
                    holdingBlock = true;

                    spriteRenderer.sprite = holdingSprite;

                    points = new GameObject[numberOfPoints];
                    for (int i = 0; i < numberOfPoints; i++)
                    {
                        points[i] = Instantiate(pointPrefab, block.transform.position, Quaternion.identity);
                    }
                }
            }
            else if (holdingBlock)
            {
                block.transform.parent = null;
                block.GetComponent<Rigidbody2D>().isKinematic = false;

                block.GetComponent<Rigidbody2D>().velocity = mouseTracker.transform.right * force;

                block.GetComponent<BoxCollider2D>().isTrigger = false;
                holdingBlock = false;

                for (int i = 0; i < numberOfPoints; i++)
                {
                    Destroy(points[i]);
                }

                spriteRenderer.sprite = normalSprite;
            }
        }
    }

    public void MousePoint(InputAction.CallbackContext context)
    {
        mousePoint = context.ReadValue<Vector2>();
    }
}
