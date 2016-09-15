using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_WallJumpState : SKMecanimState<ArcherController> {

	float timeOnState;

	public override void begin ()
	{
		base.begin ();

		timeOnState = 0;

		_context.gameplayController.enableAirControl = false;

		float horizontal = Input.GetAxis("Horizontal");

		Vector2 velocity = _context.physicsController.GetVelocity();

		_context.physicsController.AddForce(new Vector2(velocity.x,
			((Mathf.Abs(horizontal))* -1)), _context.gameplayController.wallJumpForce.y);

		//_context.physicsController.SetVelocity(_context.gameplayController.wallJumpForce);

		_context.animatorController.PlayState("Archer_Jump");
	}

	public override void reason ()
	{
		base.reason ();

		base.reason ();
		if(timeOnState > 0.2f)
		_machine.changeState<Archer_OnAirState>();
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
	}

	public override void end ()
	{
		base.end ();
	}

	public void Rotate(float pDirection){
		Vector3 currentScale = _context.transform.localScale;

		if(pDirection < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if(pDirection > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * 1;
		}
		_context.transform.localScale = currentScale;
	}
}