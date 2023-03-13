using UnityEngine;
using UnityEngine.InputSystem;

namespace Fartinate.Player.InputHandlers
{
    public class FireInputHandler : MonoBehaviour
    {
        public void OnFire(InputAction.CallbackContext callbackContext)
        {
            bool needToFire = callbackContext.ReadValueAsButton();
        }
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
