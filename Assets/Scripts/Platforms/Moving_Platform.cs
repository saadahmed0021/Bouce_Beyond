
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{
    [Header("Movement Points")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Movement Settings")]
    [SerializeField, Min(0.01f)] private float moveSpeed = 2f;
    [SerializeField, Min(0f)] private float waitTime = 0f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool movingToB = true;
    private float waitTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (pointA == null || pointB == null)
        {
            Debug.LogError("Moving_Platform: Assign Point A and Point B.", this);
            enabled = false;
            return;
        }

        targetPosition = pointB.position;

        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

    private void FixedUpdate()
    {
        if (waitTimer > 0f)
        {
            waitTimer -= Time.fixedDeltaTime;
            return;
        }

        Vector2 currentPosition = rb != null
            ? rb.position
            : (Vector2)transform.position;

        Vector2 nextPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            moveSpeed * Time.fixedDeltaTime
        );

        if (rb != null)
            rb.MovePosition(nextPosition);
        else
            transform.position = nextPosition;

        if (nextPosition == targetPosition)
        {
            movingToB = !movingToB;
            targetPosition = movingToB ? pointB.position : pointA.position;
            waitTimer = waitTime;
        }
    }
}