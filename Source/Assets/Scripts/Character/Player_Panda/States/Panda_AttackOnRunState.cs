using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_AttackOnRunState : SKMecanimState<PandaController>
{
	float timeOnState;
	
	public override void begin () {
		base.begin();
		timeOnState = 0;
		_machine.animator.Play("AttackOnRun");
	}
	
	public override void reason (){
		base.reason();
		if(timeOnState > 0.2f){
			float horizontal = Input.GetAxis("Horizontal2");
			
			if(Input.GetKey(KeyCode.UpArrow)){
				_machine.changeState<Panda_JumpState>();
				return;
			}
			
			if(horizontal == 0){
				_machine.changeState<Panda_IdleState>();
				return;
			}
			else if(horizontal != 0){
				_machine.changeState<Panda_RunState>();
				return;
			}

		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
		float horizontal = Input.GetAxis("Horizontal2");
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed
		                                                   , currentVelocity.y));

		Vector3 currentScale = _context.transform.localScale;
		
		if(horizontal < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if (horizontal > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x);
		}
		
		_context.physicsController.SetScale(currentScale);
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
