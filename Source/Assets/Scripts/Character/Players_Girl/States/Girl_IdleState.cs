using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_IdleState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		_machine.animator.Play("Idle");
	}

	public override void reason ()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded()){

			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal != 0.0f){
				_machine.changeState<Girl_RunState>();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Girl_JumpState>();
				return;
			}
			if(Input.GetKeyDown(KeyCode.F)){
				_machine.changeState<Girl_AttackState>();
				return;
			}

			_context.physicsController.SetVelocity(Vector2.zero);
		}
		else{
			_machine.changeState<Girl_OnAirState>();
			return;
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{

	}
	#endregion

	public override void end ()
	{
		base.end ();
	}
}
