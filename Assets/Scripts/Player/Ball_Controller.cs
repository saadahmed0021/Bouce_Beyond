
using UnityEngine;

namespace MaterialShift.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball_Controller : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float acceleration = 24f;
        [SerializeField] private float maximumSpeed = 8f;
        [SerializeField] private float deceleration = 30f;

        [Header("Opposite Direction Braking")]
        [SerializeField] private float initialOppositeDeceleration = 8f;
        [SerializeField] private float maximumOppositeDeceleration = 30f;

        [Header("Air Control")]
        [SerializeField] private float airControlMultiplier = 0.65f;

        [Header("Runtime")]
        [SerializeField] private float currentSpeed;

        private Rigidbody2D ballRigidbody;
        private Ball_Input ballInput;
        private Ball_Material_Controller ballMaterialController;

        private void Awake()
        {
            ballRigidbody = GetComponent<Rigidbody2D>();
            ballInput = GetComponent<Ball_Input>();
            ballMaterialController = GetComponent<Ball_Material_Controller>();
        }

        private void FixedUpdate()
        {
            float input = ballInput != null ? ballInput.MoveInput : 0f;

            float movementMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentMovementMultiplier : 1f;

            float maxSpeedMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentMaxSpeedMultiplier : 1f;

            float accelerationMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentAccelerationMultiplier : 1f;

            float decelerationMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentDecelerationMultiplier : 1f;

            float controlMultiplier = IsGrounded()
                ? 1f
                : airControlMultiplier;

            float targetSpeed = input * maximumSpeed
                * maxSpeedMultiplier * movementMultiplier;

            Vector2 velocity = ballRigidbody.linearVelocity;
            bool hasInput = Mathf.Abs(input) > 0.01f;
            bool isOpposingVelocity = hasInput && velocity.x * input < 0f;

            float targetX;

            if (isOpposingVelocity)
            {
                float speedRange = maximumSpeed * maxSpeedMultiplier * movementMultiplier;
                float speedProgress = speedRange > 0f
                    ? Mathf.Clamp01(Mathf.Abs(velocity.x) / speedRange)
                    : 0f;

                float oppositeDeceleration = Mathf.Lerp(
                    initialOppositeDeceleration,
                    maximumOppositeDeceleration,
                    speedProgress
                );

                float brakingAcceleration = Mathf.Max(
                    oppositeDeceleration,
                    deceleration
                ) * decelerationMultiplier * controlMultiplier;

                targetX = Mathf.MoveTowards(
                    velocity.x,
                    targetSpeed,
                    brakingAcceleration * Time.fixedDeltaTime
                );
            }
            else
            {
                targetX = Mathf.MoveTowards(
                    velocity.x,
                    hasInput ? targetSpeed : 0f,
                    (hasInput
                        ? acceleration * accelerationMultiplier
                        : deceleration * decelerationMultiplier)
                    * controlMultiplier
                    * Time.fixedDeltaTime
                );
            }

            ballRigidbody.linearVelocity = new Vector2(
                targetX,
                velocity.y
            );

            currentSpeed = ballRigidbody.linearVelocity.magnitude;
        }

        private bool IsGrounded()
        {
            return Mathf.Abs(ballRigidbody.linearVelocity.y) < 0.15f;
        }
    }
}
