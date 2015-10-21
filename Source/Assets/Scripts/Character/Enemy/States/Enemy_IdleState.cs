using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_IdleState : SKMecanimState<EnemyController> {

	public override void begin()
	{
		bool landed;
		base.begin ();

		if(!landed){
			landed = true;
			_context.physicsController.SetVelociy(new Vector2(0.05f, 0.0f));
		}
		_context.animatorController.PlayState("Enemy1_Idle");
	}

	public override void reason()
	{
		base.reason ();
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{

	}

	public override void end()
	{
		base.end ();
	}
	
}
