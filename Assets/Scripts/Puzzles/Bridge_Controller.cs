using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class Bridge_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bridgeBody;
    [SerializeField] private BoxCollider2D solidCollider;
    [SerializeField] private Vector2 closedPosition;
    [SerializeField] private Vector2 openPosition;
    [SerializeField, Min(0.01f)] private float movementSpeed = 3f;
    [SerializeField, Min(0f)] private float retractionDelay = 4f;

    private Vector2 targetPosition;
    private float retractionTimer;
    private bool plateOccupied;
    private bool retractionPending;

    private void Reset()
    {
        bridgeBody = GetComponent<Rigidbody2D>();
        solidCollider = GetComponent<BoxCollider2D>();
        closedPosition = transform.position;

        if (bridgeBody != null)
        {
            bridgeBody.bodyType = RigidbodyType2D.Kinematic;
            bridgeBody.gravityScale = 0f;
            bridgeBody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        if (solidCollider != null)
        {
            solidCollider.isTrigger = false;
        }
    }

    private void Awake()
    {
        if (bridgeBody == null)
        {
            bridgeBody = GetComponent<Rigidbody2D>();
        }

        if (solidCollider == null)
        {
            solidCollider = GetComponent<BoxCollider2D>();
        }

        bridgeBody.bodyType = RigidbodyType2D.Kinematic;
        bridgeBody.gravityScale = 0f;
        bridgeBody.interpolation = RigidbodyInterpolation2D.Interpolate;
        solidCollider.isTrigger = false;
        targetPosition = closedPosition;
    }

    public void SetPlateOccupied(bool occupied)
    {
        if (plateOccupied == occupied)
        {
            return;
        }

        plateOccupied = occupied;

        if (occupied)
        {
            retractionPending = false;
            targetPosition = openPosition;
            return;
        }

        retractionTimer = retractionDelay;
        retractionPending = true;
    }

    private void FixedUpdate()
    {
        if (retractionPending)
        {
            retractionTimer -= Time.fixedDeltaTime;

            if (retractionTimer <= 0f)
            {
                retractionPending = false;
                targetPosition = closedPosition;
            }
        }

        Vector2 nextPosition = Vector2.MoveTowards(
            bridgeBody.position,
            targetPosition,
            Mathf.Max(0f, movementSpeed) * Time.fixedDeltaTime);

        bridgeBody.MovePosition(nextPosition);
    }
}