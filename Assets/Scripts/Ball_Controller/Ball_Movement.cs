using UnityEngine;

public class Ball_Movement : MonoBehaviour
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

        Move();
    }

    private void Initialize()
    {
        if (references != null)
        {
            ballRigidbody = references.BallRigidbody;
        }
    }

    private void Move()
    {
        float inputValue = input.HorizontalInput;

        float targetSpeed = inputValue * ballData.maxMoveSpeed;

        float acceleration = Mathf.Abs(inputValue) > 0.01f
            ? ballData.acceleration
            : ballData.deceleration;

        if (!groundCheck.IsGrounded)
        {
            acceleration *= ballData.airControl;
        }

        float newVelocityX = Mathf.MoveTowards(
            ballRigidbody.linearVelocity.x,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        ballRigidbody.linearVelocity = new Vector2(
            newVelocityX,
            ballRigidbody.linearVelocity.y
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