using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_RunState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		//Tocar animacao IDLE.
		//Parar o personagem.
	}

	public override void reason ()
	{
		base.reason ();

		float horizontal = Input.GetAxis("Horizontal");

		if(horizontal == 0.0f)
			_machine.changeState<Girl_IdleState>();
		return;
	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal");

		Vector3 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector3(0, currentVelocity.y, 
		                                                   horizontal * _context.horizontalMovementSpeed));
	}
	#endregion

	public override void end ()
	{
		base.end ();
	}
}
