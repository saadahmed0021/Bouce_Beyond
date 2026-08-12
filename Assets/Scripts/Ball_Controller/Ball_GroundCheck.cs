using UnityEngine;

public class Ball_GroundCheck : MonoBehaviour
{
    [SerializeField] private Ball_References references;
    [SerializeField] private Ball_Data ballData;

    private bool isGrounded;

    public bool IsGrounded => isGrounded;

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        if (references == null || ballData == null)
        {
            isGrounded = false;
            return;
        }

        Transform groundCheck = references.GroundCheck;

        if (groundCheck == null)
        {
            isGrounded = false;
            return;
        }

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            ballData.groundCheckRadius,
            ballData.groundLayer
        ) != null;
    }

    private void OnDrawGizmosSelected()
    {
        if (references == null || references.GroundCheck == null || ballData == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(
            references.GroundCheck.position,
            ballData.groundCheckRadius
        );
    }
}