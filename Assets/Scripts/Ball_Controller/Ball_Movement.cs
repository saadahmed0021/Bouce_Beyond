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

    if (Mathf.Abs(inputValue) > 0.01f)
    {
        float targetSpeed = inputValue * ballData.maxMoveSpeed;

        float acceleration = ballData.acceleration;

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

        return;
    }

    ApplySmoothDeceleration();
}

private void ApplySmoothDeceleration()
{
    float currentSpeed = ballRigidbody.linearVelocity.x;

    float newSpeed = Mathf.MoveTowards(
        currentSpeed,
        0f,
        ballData.deceleration * Time.fixedDeltaTime
    );

    if (Mathf.Abs(newSpeed) < 0.01f)
    {
        newSpeed = 0f;
    }

    ballRigidbody.linearVelocity = new Vector2(
        newSpeed,
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