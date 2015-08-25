using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_IdleState : SKMecanimState<PandaController> {

	public override void begin ()
	{
		bool landed = false;
		base.begin ();

		if(!landed){
			landed =true;
			_context.physicsController.SetVelocity(new Vector2(0.05f, 0.0f));

		}if(landed){
			_context.physicsController.SetVelocity(Vector2.zero);
		}

		_machine.animator.Play("Idle");
	//	_context.physicsController.SetVelocity(Vector2.zero);
	}

	public override void reason ()
	{
		base.reason ();
		
		if(_context.physicsController.IsGrounded())
		{
			float horizontal = Input.GetAxis("Horizontal2");
			
			if(horizontal != 0.0f)
			{
				_machine.changeState<Panda_RunState>();
				return;
			}
			if(Input.GetKeyDown(KeyCode.UpArrow)){
				_machine.changeState<Panda_JumpState>();
				return;
			}

			if(Input.GetKeyDown(KeyCode.Slash)){
				_machine.changeState<Panda_AttackOnIdleState>();
			}
		}
		else
		{
			_machine.changeState<Panda_OnAirState>();
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
