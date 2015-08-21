using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_JumpState : SKMecanimState<PandaController> {

	public override void begin ()
	{
		base.begin ();
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(currentVelocity.x,
		                                                   _context.pandaJumpForce));
		_machine.animator.Play("Jump");

		
	}
	
	public override void reason(){
		base.reason ();
		_machine.changeState<Panda_OnAirState>();
		/*float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");*/

		
		
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
