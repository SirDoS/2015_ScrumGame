using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_RunState : SKMecanimState<PandaController> {

	public override void begin ()
	{
		base.begin ();
		
		_machine.animator.Play("Run");
	}
	
	public override void reason ()
	{
		base.reason ();
		
		if(_context.physicsController.IsGrounded()){
			
			float horizontal = Input.GetAxis("Horizontal2");
			
			if(horizontal == 0.0f){
				_machine.changeState<Panda_IdleState>();
				return;
			}

			if(horizontal < 0.0f){
				_context.transform.localScale = new Vector3(-1,1,1);
			}else if (horizontal > 0.0f){
				_context.transform.localScale = new Vector3(1,1,1);
			}

			if(Input.GetKeyDown(KeyCode.UpArrow)){
				_machine.changeState<Panda_JumpState>();
				return;
			}
		}else{
			_machine.changeState<Panda_OnAirState>();
			return;
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal2");
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed
		                                                   , currentVelocity.y));
	}
	#endregion
	
	public override void end ()
	{
		base.end ();
	}
}
