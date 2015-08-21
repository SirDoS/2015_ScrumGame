using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_OnAirState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		_machine.animator.Play("OnAir");
	}
	
	public override void reason ()
	{
		float horizontal = Input.GetAxis("Horizontal");

		if(_context.physicsController.IsGrounded()){
			_machine.changeState<Girl_IdleState>();
			return;
		}
		if (horizontal != 0.0f){
			Vector2 currentVelocity = _context.physicsController.GetVelocity();
			_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
			                                                   currentVelocity.y));
		}
		base.reason ();
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
