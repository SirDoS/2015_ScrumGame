using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_RunState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		_machine.animator.Play("Run");

	}
	public override void reason ()
	{
		base.reason ();
		if(_context.physicsController.IsGrounded()){

			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal == 0.0f){
				_machine.changeState<Girl_IdleState>();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Girl_JumpState>();
				return;
			}
		}else{
			_machine.changeState<Girl_OnAirState>();
			return;
		}

	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal");


		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
		                                                   currentVelocity.y));
	}
	#endregion

	public override void end ()
	{
		base.end ();
	}
}
