using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_WallJumpState : SKMecanimState<GirlController>
{
	public override void begin () {
		base.begin();
		
		//_machine.animator.Play("WallJump");
	}
	
	public override void reason (){
		base.reason();
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}