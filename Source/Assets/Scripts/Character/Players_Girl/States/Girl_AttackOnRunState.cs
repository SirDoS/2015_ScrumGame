using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackOnRunState : SKMecanimState<GirlController>
{
	float timeOnState;
	
	public override void begin () {
		base.begin();
		timeOnState = 0;
		_machine.animator.Play("Attack");
	}
	
	public override void reason (){
		base.reason();



		if(timeOnState > 0.5f){
			
			float horizontal = Input.GetAxis("Horizontal");
			
			if(horizontal == 0){
				_machine.changeState<Girl_IdleState>();
				return;
			}else if(horizontal != 0){
				_machine.changeState<Girl_RunState>();
				return;
			}
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;

		float horizontal = Input.GetAxis("Horizontal");

		Vector2 currentVelocity = _context.physicsController.GetVelocity();
				_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
			                                                   currentVelocity.y));
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
