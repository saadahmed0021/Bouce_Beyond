using UnityEngine;

[CreateAssetMenu(fileName = "Ball_Data", menuName = "Ball/Ball Data")]
public class Ball_Data : ScriptableObject
{
    [Header("Movement")]
    [Min(0f)] public float maxMoveSpeed = 8f;
    [Min(0f)] public float acceleration = 25f;
    [Min(0f)] public float deceleration = 30f;
    [Range(0f, 1f)] public float airControl = 0.7f;

    [Header("Rolling")]
    [Min(0f)] public float torqueForce = 20f;
    [Min(0f)] public float maxAngularVelocity = 1000f;

    [Header("Jump")]
    [Min(0f)] public float jumpForce = 12f;

    [Header("Physics")]
    [Min(0f)] public float gravityScale = 3f;
    [Range(0f, 1f)] public float groundFriction = 0.8f;

    [Header("Ground Check")]
    [Min(0f)] public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Advanced")]
    [Min(0f)] public float maxFallSpeed = 20f;
    [Min(0f)] public float coyoteTime = 0.1f;
    [Min(0f)] public float jumpBufferTime = 0.1f;
}