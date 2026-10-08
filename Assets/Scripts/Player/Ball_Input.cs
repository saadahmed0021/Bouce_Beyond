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

        private void Update()
        {
            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
            {
                MoveInput = 0f;
                JumpInput = false;
                return;
            }

            MoveInput = 0f;

            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
            {
                MoveInput -= 1f;
            }

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
            {
                MoveInput += 1f;
            }

            JumpInput = keyboard.spaceKey.isPressed;
        }
    }
}
