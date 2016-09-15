using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_OnDeathState : SKMecanimState<ArcherController> {

	float respawnTime;

	public override void begin ()
	{
		base.begin ();

		respawnTime = 0;
		_context.physicsController.SetVelocity(Vector2.zero);

		_context.animatorController.PlayState("Archer_Death");
	}

	public override void reason ()
	{
		base.reason ();
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		respawnTime += deltaTime;
		if(respawnTime > 2.0f)
		{
			_context.currentLife = 100;
			_context.isAlive = true;
			_machine.changeState<Archer_IdleState>();
		}
	}

	public override void end ()
	{
		base.end ();
	}
}