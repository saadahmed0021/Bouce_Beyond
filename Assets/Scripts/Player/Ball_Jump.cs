using UnityEngine;

namespace MaterialShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball_Jump : MonoBehaviour
    {
        [Header("Jump")]
        [SerializeField] private float jumpVelocity = 12f;
        [SerializeField] private float jumpBufferTime = 0.12f;
        [SerializeField] private float coyoteTime = 0.1f;

        [Header("Variable Jump")]
        [SerializeField] private float jumpCutAcceleration = 28f;

        [Header("Fall Control")]
        [SerializeField] private float maximumFallSpeed = 18f;

        private Rigidbody2D ballRigidbody;
        private Ball_Input ballInput;
        private Ball_Ground_Check groundCheck;
        private Ball_Material_Controller ballMaterialController;

        private float jumpBufferRemaining;
        private float coyoteRemaining;
        private bool jumpWasHeld;

        private void Awake()
        {
            ballRigidbody = GetComponent<Rigidbody2D>();
            ballInput = GetComponent<Ball_Input>();
            groundCheck = GetComponent<Ball_Ground_Check>();
            ballMaterialController = GetComponent<Ball_Material_Controller>();
        }

        private void Update()
        {
            bool jumpIsHeld = ballInput != null && ballInput.JumpInput;

            if (jumpIsHeld && !jumpWasHeld)
            {
                jumpBufferRemaining = jumpBufferTime;
            }

            jumpWasHeld = jumpIsHeld;
        }

        private void FixedUpdate()
        {
            if (groundCheck != null && groundCheck.IsGrounded)
            {
                coyoteRemaining = coyoteTime;
            }
            else
            {
                coyoteRemaining = Mathf.Max(0f, coyoteRemaining - Time.fixedDeltaTime);
            }

            jumpBufferRemaining = Mathf.Max(0f, jumpBufferRemaining - Time.fixedDeltaTime);

            if (jumpBufferRemaining > 0f && coyoteRemaining > 0f)
            {
                PerformJump();
                return;
            }

            ApplyVariableJump();
            ApplyMaximumFallSpeed();
        }

        private void PerformJump()
        {
            float horizontalVelocity = ballRigidbody.linearVelocity.x;
            float jumpMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentJumpMultiplier
                : 1f;
            ballRigidbody.linearVelocity = new Vector2(
                horizontalVelocity,
                jumpVelocity * jumpMultiplier);

            jumpBufferRemaining = 0f;
            coyoteRemaining = 0f;
        }

        private void ApplyVariableJump()
        {
            if (ballInput != null && !ballInput.JumpInput && ballRigidbody.linearVelocity.y > 0f)
            {
                float newVerticalVelocity = Mathf.MoveTowards(
                    ballRigidbody.linearVelocity.y,
                    0f,
                    jumpCutAcceleration * Time.fixedDeltaTime);

                ballRigidbody.linearVelocity = new Vector2(
                    ballRigidbody.linearVelocity.x,
                    newVerticalVelocity);
            }
        }

        private void ApplyMaximumFallSpeed()
        {
            float verticalVelocity = ballRigidbody.linearVelocity.y;

            if (verticalVelocity < -maximumFallSpeed)
            {
                ballRigidbody.linearVelocity = new Vector2(
                    ballRigidbody.linearVelocity.x,
                    -maximumFallSpeed);
            }
        }
    }
}
