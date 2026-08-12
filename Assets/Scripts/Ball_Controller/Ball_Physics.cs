using UnityEngine;

public class Ball_Physics : MonoBehaviour
{
    [SerializeField] private Ball_References references;
    [SerializeField] private Ball_Input input;
    [SerializeField] private Ball_Data ballData;

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

        ApplyRolling();
        LimitAngularVelocity();
    }

    private void Initialize()
    {
        if (references != null)
        {
            ballRigidbody = references.BallRigidbody;
        }

        if (ballRigidbody != null)
        {
            ballRigidbody.gravityScale = ballData != null
                ? ballData.gravityScale
                : ballRigidbody.gravityScale;
        }
    }

    private void ApplyRolling()
    {
        float horizontalInput = input.HorizontalInput;

        if (Mathf.Abs(horizontalInput) < 0.01f)
        {
            return;
        }

        float torque = -horizontalInput * ballData.torqueForce;

        ballRigidbody.AddTorque(torque, ForceMode2D.Force);
    }

    private void LimitAngularVelocity()
    {
        float maxAngularVelocity = ballData.maxAngularVelocity;

        if (Mathf.Abs(ballRigidbody.angularVelocity) > maxAngularVelocity)
        {
            ballRigidbody.angularVelocity = Mathf.Sign(
                ballRigidbody.angularVelocity
            ) * maxAngularVelocity;
        }
    }

    private bool IsReady()
    {
        return references != null &&
               input != null &&
               ballData != null &&
               ballRigidbody != null;
    }
}