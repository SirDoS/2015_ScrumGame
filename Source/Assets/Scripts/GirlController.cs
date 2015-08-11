using UnityEngine;
using System.Collections;

public class GirlController : BaseChar
{
	public bool isGrounded;
	public float jumpForce;
	
	void Start()
	{
		currentLife = maxLife;
		isAlive = true;
	}
	
	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		Vector3 currentVelocity = physicsController.GetVelocity();
		physicsController.SetVelocity(new Vector3(0,currentVelocity.y , horizontal * horizontalMovementSpeed));
	}
}