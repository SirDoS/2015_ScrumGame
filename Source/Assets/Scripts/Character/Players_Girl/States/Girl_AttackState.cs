using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackState : SKMecanimState<GirlController>
{
	public override void begin () {
		base.begin();
		Debug.Log("Im attacking, bitch!");
		//_machine.animator.Play("Attack");
	}
	
	public override void reason (){
		base.reason();

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if(horizontal == 0){
			_machine.changeState<Girl_IdleState>();
			return;
		}
		if(horizontal != 0){
			_machine.changeState<Girl_RunState>();
			return;
		}if(vertical > 0){
			_machine.changeState<Girl_JumpState>();
			return;
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
