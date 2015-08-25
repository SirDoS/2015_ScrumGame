using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_WallJumpState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		
		_machine.animator.Play("WallJump");
		
	}
	public override void reason ()
	{
		base.reason ();
		
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal");
				
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
		                                                   currentVelocity.y));

		if(horizontal > 0.0f){
			if(Input.GetKeyDown(KeyCode.Space) && isWallAhead()){

			}
		}
	}
	#endregion

	public bool isWallAhead(){
		return true;
	}
	
	public override void end ()
	{
		base.end ();
	}

}