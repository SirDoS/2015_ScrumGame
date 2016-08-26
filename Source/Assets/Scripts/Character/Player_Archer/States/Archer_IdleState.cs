using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_IdleState : SKMecanimState<ArcherController> {

	public override void begin ()
	{
		bool landed = false;
		base.begin ();

		if(!landed){
			landed = true;
			_context.physicsController.SetVelocity(new Vector2(0.05f, 0.0f));

		}

		if(landed){
			_context.physicsController.SetVelocity(Vector2.zero);
		}

		_context.animatorController.PlayState("Archer_Idle");
	}

	public override void reason ()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded())
		{
			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal != 0.0f)
			{
				_machine.changeState<Archer_RunState>();
				return;
			}
			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Archer_JumpState>();
				return;
			}
			/*
			if(Input.GetKeyDown(KeyCode.Slash)){
				_machine.changeState<Archer_AttackOnIdleState>();
			}
			*/

			_context.physicsController.SetVelocity(Vector2.zero);
		}
		else
		{
			_machine.changeState<Archer_OnAirState>();
			return;
		}
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}

	public override void end ()
	{
		base.end ();
	}
}