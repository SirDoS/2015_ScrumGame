using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_JumpState : SKMecanimState<GirlController>
{
	//public bool jumped = false;
	public override void begin ()
	{
		base.begin ();
		
		Vector3 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector3(currentVelocity.x,
		                                                   _context.girlJumpForce,
		                                                   0));
		//Tocar animacao JUMP.

	}

	public override void reason(){
		base.reason ();

		/*float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");*/

		//Independente de qualquer condicao, passara para OnAirState
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
