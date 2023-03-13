using UnityEngine;
using UnityEngine.InputSystem;

namespace Fartinate.Player.InputHandlers
{
    public class LookInputHandler : MonoBehaviour
    {
        public Transform rotateVertical;
        public Transform rotateHorizontal;

        public float sense;
        
        public void OnLook(InputAction.CallbackContext callbackContext)
        {
            if(!callbackContext.performed)
                return;
            Vector2 delta = callbackContext.ReadValue<Vector2>();
            
            float deltaX = delta.x;
            float deltaY = -delta.y;

            rotateVertical.Rotate(deltaY*sense, 0, 0);
            rotateHorizontal.Rotate(0, deltaX*sense, 0);
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                UnityEngine.Cursor.lockState = CursorLockMode.Locked;
                UnityEngine.Cursor.visible = false;
            }
            else
            {
                UnityEngine.Cursor.lockState = CursorLockMode.None;
                UnityEngine.Cursor.visible = true;
            }
        }
    }
}
