using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Fartinate.Player.InputHandlers
{
	public class MoveInputHandler : MonoBehaviour
	{
		public Rigidbody toMove;
		public float maxSpeed;
		public float movingForce;
		public float forceOfStopping;

		private Vector2 _needForSpeed = Vector2.zero;

		public void OnMoveForwardBackward(InputAction.CallbackContext callbackContext)
		{
			if (callbackContext.performed)
				return;
			float input = callbackContext.ReadValue<float>();
			_needForSpeed.y = input*maxSpeed;
		}
		
		public void OnMoveLeftRight(InputAction.CallbackContext callbackContext)
		{
			if (callbackContext.performed)
				return;
			float input = callbackContext.ReadValue<float>();
			_needForSpeed.x = input*maxSpeed;
		}

		private float CalculateDeltaForce(float neededSpeed, float nowSpeed)
		{
			if (neededSpeed == 0)
			{
				if (nowSpeed > 0)
				{
					return Mathf.Clamp(nowSpeed * -forceOfStopping, -forceOfStopping, 0);
				}

				if (nowSpeed < 0)
				{
					return Mathf.Clamp(-nowSpeed * forceOfStopping, 0, forceOfStopping);
				}

				return 0;
			}
			
			if (Math.Abs(nowSpeed - neededSpeed) < 1)
				return 0;
					
			if (nowSpeed < neededSpeed)
				return movingForce; //Mathf.Clamp(movingForce * 30/nowSpeed, 0, movingForce);

			if (nowSpeed > neededSpeed)
				return -movingForce; //Mathf.Clamp(-movingForce * 30/nowSpeed, -movingForce, 0);

			;
			
			throw new Exception("CalculateDeltaForce bug");
		}
		
		private void FixedUpdate()
		{
			var localVelocity = toMove.GetLocalVelocity();
			
			Vector3 deltaForce = new Vector3(
				CalculateDeltaForce(_needForSpeed.x, localVelocity.x),
				0,
				CalculateDeltaForce(_needForSpeed.y, localVelocity.z));
			
			//deltaForce.Scale(new Vector3(Time.fixedDeltaTime, 0, Time.fixedDeltaTime));
			
			toMove.AddRelativeForce(deltaForce, ForceMode.Impulse);
		}
	}
}