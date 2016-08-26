using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_AttackOnIdleState : SKMecanimState<PandaController>
{
	float timeOnState;

	public override void begin () {
		base.begin();
		timeOnState = 0;
		_context.animatorController.PlayState("Ronin_Attack");
		_context.attackController.Attack();
	}
	
	public override void reason (){
		base.reason();
		if(timeOnState > 0.5f){
			float horizontal = Input.GetAxis("Horizontal2");
			if(Input.GetKey(KeyCode.UpArrow)){
				_machine.changeState<Panda_JumpState>();
				return;
			}
			
			if(horizontal == 0){
				_machine.changeState<Panda_IdleState>();
				return;
			}
			if(horizontal != 0){
				_machine.changeState<Panda_RunState>();
				return;
			}
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
