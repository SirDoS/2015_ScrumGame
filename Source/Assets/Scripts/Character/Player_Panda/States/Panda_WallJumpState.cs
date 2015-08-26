using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_WallJumpState : SKMecanimState<PandaController>
{
	float timeOnState;

	public override void begin ()
	{
		base.begin ();

		timeOnState = 0;

		_context.gameplayController.enableAirControl = false;
		
		float horizontal = Input.GetAxis("Horizontal2");
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.AddForce(new Vector2(currentVelocity.x,
		                                                ((Mathf.Abs(horizontal))* -1)), -75f);
		
		_machine.animator.Play("WallJump");
	}
	public override void reason ()
	{
		base.reason ();
		if(timeOnState > 0.2f)
		_machine.changeState<Panda_OnAirState>();
		
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
	}
	#endregion
	
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