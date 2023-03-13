using UnityEngine;

public static class LocalVectorValues
{
	public static Vector3 GetLocalVelocity(this Rigidbody rigidbody)
	{
		return rigidbody.transform.InverseTransformDirection(rigidbody.velocity);
	}
}

public class Moving : MonoBehaviour
{
	public Rigidbody toMove;
	public Collider playerCollider;
	public float maxSpeed;
	public float jumpForce;
	public float maxDistanceToFloorForJump;
	public float movingForce;
	public float forceOfStopping;

	private float ToIntFromZero(float input)
	{
		if (input > 0)
			return 1f;
		if (input < 0)
			return -1f;
		return 0f;
	}

	private void FixedUpdate()
	{
		float rightInput = ToIntFromZero(Input.GetAxis("Horizontal"));
		float forwardInput = ToIntFromZero(Input.GetAxis("Vertical"));

		float rightForceToAdd = HandleMove(toMove.GetLocalVelocity().x, rightInput);
		float forwardForceToAdd = HandleMove(toMove.GetLocalVelocity().z, forwardInput);

		float jumpInput = ToIntFromZero(Input.GetAxis("Jump"));
		float upForceToAdd = HandleJump(jumpInput);

		toMove.AddRelativeForce(rightForceToAdd, upForceToAdd, forwardForceToAdd, ForceMode.VelocityChange);
	}

	private float HandleJump(float jump)
	{
		float upForceToAdd = 0;
		if (jump > 0)
		{
			int layerMask = 1 << gameObject.layer;
			layerMask = ~layerMask;
			if (Physics.Raycast(transform.position + new Vector3(0, -1, 0), transform.TransformDirection(Vector3.down)
				    /*Vector3.down*/ /*transform.up*-1*/, out _, maxDistanceToFloorForJump, layerMask))
			{
				upForceToAdd += jumpForce;
			}
		}

		return upForceToAdd;
	}


	// localVelocity:
	//	rigidbodyToMove.transform.InverseTransformDirection(rigidbodyToMove.velocity).x
	//
	// inputValue:
	// Input.GetAxis("Horizontal")
	private float HandleMove(float localVelocity, float inputValue)
	{
		float relativeForceToAdd = 0;

		if (inputValue > 0 && localVelocity >= 0 && localVelocity < maxSpeed)
		{
			relativeForceToAdd += inputValue * movingForce;
		}
		else if (inputValue < 0 && localVelocity <= 0 && localVelocity > -maxSpeed)
		{
			relativeForceToAdd += inputValue * movingForce;
		}
		else
		{
			if (localVelocity > 0)
			{
				relativeForceToAdd += Mathf.Clamp(-localVelocity * 50, -forceOfStopping, 0);
			}
			else if (localVelocity < 0)
			{
				relativeForceToAdd += Mathf.Clamp(-localVelocity * 50, 0, forceOfStopping);
			}
		}

		return relativeForceToAdd * Time.fixedDeltaTime;
	}
}