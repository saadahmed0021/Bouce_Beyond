using UnityEngine;

namespace MaterialShift.Player
{
    public class Ball_Ground_Check : MonoBehaviour
    {
        [Header("Ground Check")]
        [SerializeField] private Transform groundCheckPoint;
        [SerializeField] private float groundCheckRadius = 0.15f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private bool isGrounded;

        public bool IsGrounded => isGrounded;

        private void FixedUpdate()
        {
            isGrounded = Physics2D.OverlapCircle(
                groundCheckPoint.position,
                groundCheckRadius,
                groundLayer) != null;
        }

        private void OnDrawGizmosSelected()
        {
            if (groundCheckPoint == null)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}
