using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_JumpState : SKMecanimState<GirlController>
{
	public bool jumped = false;
	public override void begin ()
	{
		base.begin ();
		
		//Tocar animacao JUMP.

	}

	public override void reason(){
		base.reason ();

		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		//Independente de qualquer condicao, passara para OnAirState
		if(jumped)
		_machine.changeState<Girl_OnAirState>();


	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float vertical = Input.GetAxis("Vertical");
		
		Vector3 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector3(0,
		                                                   vertical * _context.verticalMovementSpeed,
		                                                   currentVelocity.z));
		jumped = true;
	}
	#endregion

	public override void end ()
	{
		base.end ();
	}

}
