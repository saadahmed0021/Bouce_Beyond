using UnityEngine;

public class Ball_References : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D ballRigidbody;
    [SerializeField] private CircleCollider2D ballCollider;

    [Header("Transforms")]
    [SerializeField] private Transform groundCheck;

    public Rigidbody2D BallRigidbody => ballRigidbody;
    public CircleCollider2D BallCollider => ballCollider;
    public Transform GroundCheck => groundCheck;

    private void Awake()
    {
        CacheReferences();
    }

    private void CacheReferences()
    {
        if (ballRigidbody == null)
        {
            ballRigidbody = GetComponent<Rigidbody2D>();
        }

        if (ballCollider == null)
        {
            ballCollider = GetComponent<CircleCollider2D>();
        }
    }
}