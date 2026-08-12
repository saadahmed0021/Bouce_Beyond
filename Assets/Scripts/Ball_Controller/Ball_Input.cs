using UnityEngine;

public class Ball_Input : MonoBehaviour
{
    private float horizontalInput;
    private bool jumpPressed;

    public float HorizontalInput => horizontalInput;
    public bool JumpPressed => jumpPressed;

    private void Update()
    {
        ReadMovementInput();
        ReadJumpInput();
    }

    private void ReadMovementInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void ReadJumpInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }

    public void ConsumeJumpInput()
    {
        jumpPressed = false;
    }
}