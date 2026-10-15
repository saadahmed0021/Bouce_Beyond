
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

        [Header("Air Control")]
        [SerializeField] private float airControlMultiplier = 0.65f;

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

            float targetX = Mathf.MoveTowards(
                velocity.x,
                Mathf.Abs(input) > 0.01f ? targetSpeed : 0f,
                (Mathf.Abs(input) > 0.01f
                    ? acceleration * accelerationMultiplier
                    : deceleration * decelerationMultiplier)
                * controlMultiplier
                * Time.fixedDeltaTime
            );

            ballRigidbody.linearVelocity = new Vector2(
                targetX,
                velocity.y
            );
        }

        private bool IsGrounded()
        {
            return Mathf.Abs(ballRigidbody.linearVelocity.y) < 0.15f;
        }
    }
}
