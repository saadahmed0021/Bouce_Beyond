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

        [Header("Rolling")]
        [SerializeField] private float rotationFactor = 1.25f;

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
                ? ballMaterialController.CurrentMovementMultiplier
                : 1f;
            float maxSpeedMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentMaxSpeedMultiplier
                : 1f;
            float accelerationMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentAccelerationMultiplier
                : 1f;
            float decelerationMultiplier = ballMaterialController != null
                ? ballMaterialController.CurrentDecelerationMultiplier
                : 1f;
            float effectiveMaximumSpeed = maximumSpeed * maxSpeedMultiplier;
            float targetSpeed = input * effectiveMaximumSpeed * airControlMultiplier * movementMultiplier;
            float horizontalVelocity = ballRigidbody.linearVelocity.x;

            if (Mathf.Abs(input) > 0.01f)
            {
                horizontalVelocity = Mathf.MoveTowards(
                    horizontalVelocity,
                    targetSpeed,
                    acceleration * accelerationMultiplier * Time.fixedDeltaTime);
            }
            else
            {
                horizontalVelocity = Mathf.MoveTowards(
                    horizontalVelocity,
                    0f,
                    deceleration * decelerationMultiplier * Time.fixedDeltaTime);
            }

            ballRigidbody.linearVelocity = new Vector2(horizontalVelocity, ballRigidbody.linearVelocity.y);
            ballRigidbody.angularVelocity = -horizontalVelocity * rotationFactor;
        }
    }
}
