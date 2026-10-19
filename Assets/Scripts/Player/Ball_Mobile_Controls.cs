using UnityEngine;
using UnityEngine.EventSystems;

namespace MaterialShift.Player
{
    public class Ball_Mobile_Controls : MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerExitHandler
    {
        public enum Control
        {
            Left,
            Right,
            Jump
        }

        [SerializeField] private Ball_Input ballInput;
        [SerializeField] private Control control;

        private bool isPressed;

        public void OnPointerDown(PointerEventData eventData)
        {
            SetPressed(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SetPressed(false);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            SetPressed(false);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                SetPressed(false);
            }
        }

        private void OnApplicationPause(bool isPaused)
        {
            if (isPaused)
            {
                SetPressed(false);
            }
        }

        private void OnDisable()
        {
            SetPressed(false);
        }

        private void SetPressed(bool pressed)
        {
            if (isPressed == pressed)
            {
                return;
            }

            isPressed = pressed;

            if (ballInput == null)
            {
                return;
            }

            switch (control)
            {
                case Control.Left:
                    ballInput.SetMobileLeftHeld(pressed);
                    break;
                case Control.Right:
                    ballInput.SetMobileRightHeld(pressed);
                    break;
                case Control.Jump:
                    ballInput.SetMobileJumpHeld(pressed);
                    break;
            }
        }
    }
}