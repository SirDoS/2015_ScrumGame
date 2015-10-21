using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_OnDeathState : SKMecanimState<PandaController> {
	float respawnTime;
	public override void begin ()
	{
		base.begin ();
		respawnTime = 0;
		_context.physicsController.SetVelocity(Vector2.zero);

		_context.animatorController.PlayState("OnDeath");
	}

	public override void reason ()
	{
		base.reason ();
	}

	#region implemented abstract members of SKMecanimState

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		respawnTime += deltaTime;
		if(respawnTime > 2.0f)
		{
			_context.currentLife = 100;
			_context.isAlive = true;
			_machine.changeState<Panda_IdleState>();
		}
	}

	#endregion
}
