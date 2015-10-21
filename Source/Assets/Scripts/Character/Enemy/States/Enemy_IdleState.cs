using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_IdleState : SKMecanimState<EnemyController> {

	public override void begin()
	{
		bool landed = false;
		base.begin ();

		if(!landed){
			landed = true;
			_context.physicsController.SetVelocity(new Vector2(0.05f, 0.0f));
		}
		if(landed){
			_context.physicsController.SetVelocity(Vector2.zero);
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
