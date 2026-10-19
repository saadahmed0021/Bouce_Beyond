using UnityEngine;
using UnityEngine.InputSystem;

namespace MaterialShift.Player
{
    /// <summary>
    /// Reads player input and exposes it to the ball controllers.
    /// </summary>
    public class Ball_Input : MonoBehaviour
    {
        public float MoveInput { get; private set; }

        public bool JumpInput { get; private set; }

        private bool mobileLeftHeld;
        private bool mobileRightHeld;
        private bool mobileJumpHeld;

        public void SetMobileLeftHeld(bool isHeld)
        {
            mobileLeftHeld = isHeld;
        }

        public void SetMobileRightHeld(bool isHeld)
        {
            mobileRightHeld = isHeld;
        }

        public void SetMobileJumpHeld(bool isHeld)
        {
            mobileJumpHeld = isHeld;
        }

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;
            float keyboardMove = 0f;
            bool keyboardJump = false;

            if (keyboard != null)
            {
                if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                {
                    keyboardMove -= 1f;
                }

                if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                {
                    keyboardMove += 1f;
                }

                keyboardJump = keyboard.spaceKey.isPressed;
            }

            float mobileMove = (mobileRightHeld ? 1f : 0f)
                - (mobileLeftHeld ? 1f : 0f);
            MoveInput = Mathf.Clamp(keyboardMove + mobileMove, -1f, 1f);
            JumpInput = keyboardJump || mobileJumpHeld;
        }
    }
}
