using UnityEngine;
using UnityEngine.Serialization;

namespace MaterialShift.Player
{
    public class Ball_Ground_Check : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] private Transform groundCheckPoint;
        [FormerlySerializedAs("groundCheckRadius")]
        [SerializeField] private float groundCheckDistance = 0.15f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool isGrounded;

        public bool IsGrounded => isGrounded;

        private void FixedUpdate()
        {
            isGrounded = groundCheckPoint != null && Physics2D.Raycast(
                groundCheckPoint.position,
                Vector2.down,
                groundCheckDistance,
                groundLayer);
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            Vector3 checkPoint = groundCheckPoint.position;
            Gizmos.DrawLine(checkPoint, checkPoint + Vector3.down * groundCheckDistance);
        }
    }
}
