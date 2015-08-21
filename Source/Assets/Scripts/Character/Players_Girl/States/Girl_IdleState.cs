using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_IdleState : SKMecanimState<GirlController>
{
//	bool landed = false;

	public override void begin ()
	{
		base.begin ();

//		if(!landed){
//			landed = true;
//			_context.physicsController.SetVelocity(new Vector2(0.07f, 0.0f));
//		}else{
//			_context.physicsController.SetVelocity(Vector2.zero);
//		}

		_machine.animator.Play("Idle");
	}

	public override void reason ()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded())
		{
			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal != 0.0f)
			{
				_machine.changeState<Girl_RunState>();
				return;
			}
//			else
//				

			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Girl_JumpState>();
				return;
			}

			_context.physicsController.SetVelocity(Vector2.zero);
		}
		else
		{
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
