using UnityEngine;

public class Ball_Jump : MonoBehaviour
{
    [SerializeField] private Ball_References references;
    [SerializeField] private Ball_Input input;
    [SerializeField] private Ball_Data ballData;
    [SerializeField] private Ball_GroundCheck groundCheck;

    private Rigidbody2D ballRigidbody;

    private void Awake()
    {
        Initialize();
    }

    private void FixedUpdate()
    {
        if (!IsReady())
        {
            return;
        }

        HandleJump();
    }

    private void Initialize()
    {
        if (references != null)
        {
            ballRigidbody = references.BallRigidbody;
        }
    }

    private void HandleJump()
    {
        if (!input.JumpPressed)
        {
            return;
        }

        if (!groundCheck.IsGrounded)
        {
            return;
        }

        PerformJump();
        input.ConsumeJumpInput();
    }

    private void PerformJump()
    {
        Vector2 velocity = ballRigidbody.linearVelocity;

        velocity.y = 0f;
        ballRigidbody.linearVelocity = velocity;

        ballRigidbody.AddForce(
            Vector2.up * ballData.jumpForce,
            ForceMode2D.Impulse
        );
    }

    private bool IsReady()
    {
        return references != null &&
               input != null &&
               ballData != null &&
               groundCheck != null &&
               ballRigidbody != null;
    }
}