using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_JumpState : SKMecanimState<ArcherController> {

	public override void begin ()
	{
		base.begin ();

		base.begin ();

		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.AddForce(new Vector2(currentVelocity.x,
			_context.physicsController.JumpForce), 2);

		_context.animatorController.PlayState("Archer_Jump");
	}

	public override void reason ()
	{
		base.reason ();

		_machine.changeState<Archer_OnAirState>();
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}

	public override void end ()
	{
		base.end ();
	}
}