using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_JumpState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(currentVelocity.x,
		                                                   _context.girlJumpForce));

		//_machine.animator.Play("Jump");
		//Tocar animacao JUMP.

	}

	public override void reason(){
		base.reason ();
		_machine.changeState<Girl_OnAirState>();
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
