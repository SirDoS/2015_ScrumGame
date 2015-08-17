using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class GirlController : BaseChar
{
	public float girlJumpForce = 500.0f;

	public SKMecanimStateMachine<GirlController> stateMachine;

	void Start()
	{
		currentLife = maxLife;
		isAlive = true;

		stateMachine = new SKMecanimStateMachine<GirlController>(animatorController.Animator, this, new Girl_IdleState());
		stateMachine.addState(new Girl_IdleState());
		stateMachine.addState(new Girl_RunState());
		stateMachine.addState(new Girl_JumpState());
		stateMachine.addState(new Girl_OnAirState());
		stateMachine.addState(new Girl_LandState());

		/*stateMachine.onStateChanged += () => {
			Debug.Log(stateMachine.currentState.ToString());
		};*/

	}
	
	void Update()
	{
		stateMachine.update(Time.deltaTime);

//		float horizontal = Input.GetAxis("Horizontal");
//		float vertical = Input.GetAxis("Vertical");
//
//		Vector3 currentVelocity = physicsController.GetVelocity();
//		physicsController.SetVelocity(new Vector3(0,
//		                                          currentVelocity.y ,
//		                                          horizontal * horizontalMovementSpeed));
//		if(Input.GetKeyDown(KeyCode.Space)){
//			physicsController.AddForce(new Vector3(0.0f, 1.0f, 0.0f), girlJumpForce);
//		}
	}
}