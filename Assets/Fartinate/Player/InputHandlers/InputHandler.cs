using UnityEngine;
using UnityEngine.InputSystem;

namespace Fartinate.Player.InputHandlers
{
    public static class LocalVectorValues
    {
        public static Vector3 GetLocalVelocity(this Rigidbody rigidbody)
        {
            return rigidbody.transform.InverseTransformDirection(rigidbody.velocity);
        }
        
        public static string Serialize(this Vector2 vector2)
        {
            return $"({vector2.x} : {vector2.y})";
        }
    }
    
    public class InputHandler : MonoBehaviour
    {
        public Transform toJackalitWhenSeat;
        public float highMultiplierWhenSeat;

        public Rigidbody rigidbodyToJump;
        public float jumpForce;
        public float maxDistanceToFloorForJump;
        
        public void OnJump(InputAction.CallbackContext callbackContext)
        {
            if(!callbackContext.performed)
                return;
            
            //нажатие Space = вызов этого метода
            Debug.Log("Space");
            int layerMask = 1 << gameObject.layer;
            layerMask = ~layerMask;
            if (Physics.Raycast(transform.position, 
                    transform.TransformDirection(Vector3.down), out _, maxDistanceToFloorForJump, layerMask))
            {
                Debug.Log("Jump");
                rigidbodyToJump.AddRelativeForce(0, jumpForce, 0, ForceMode.VelocityChange);
            }
        }

        private bool _needToSeat = false;
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus && _needToSeat)
            {
                _needToSeat = false;
                Seat();
            }
        }
        private void Seat()
        {
            Debug.Log(_needToSeat);
            if (_needToSeat)
            {
                var localScale = toJackalitWhenSeat.localScale;
                var newLocalScale = new Vector3(localScale.x, localScale.y * highMultiplierWhenSeat, localScale.z);
                toJackalitWhenSeat.localScale = newLocalScale;
            }
            else
            {
                var localScale = toJackalitWhenSeat.localScale;
                var newLocalScale = new Vector3(localScale.x, localScale.y / highMultiplierWhenSeat, localScale.z);
                toJackalitWhenSeat.localScale = newLocalScale;
            }
        }
        public void OnSeat(InputAction.CallbackContext callbackContext)
        {
            if(!callbackContext.performed)
                return;
            
            _needToSeat = callbackContext.ReadValueAsButton();
            Seat();
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
