using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_OnAirState : SKMecanimState<PandaController>{
	public override void begin ()
	{
		base.begin ();
		_machine.animator.Play("OnAir");
	}
	
	public override void reason ()
	{
		base.reason ();

		float horizontal = Input.GetAxis("Horizontal2");
		
		if(_context.physicsController.IsGrounded()){
			_machine.changeState<Panda_IdleState>();
			return;
		}
		if (horizontal != 0.0f){
			Vector2 currentVelocity = _context.physicsController.GetVelocity();
			_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
			                                                   currentVelocity.y));

			Vector3 currentScale = _context.transform.localScale;
			
			if(horizontal < 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x) * -1;
			}else if (horizontal > 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x) * 1;
			}
			
			_context.transform.localScale = currentScale;
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
