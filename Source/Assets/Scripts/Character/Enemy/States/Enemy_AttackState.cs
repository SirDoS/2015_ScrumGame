using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_AttackState : SKMecanimState<EnemyController> {
	
	public override void begin()
	{
		base.begin ();

		_context.animatorController.PlayState("Enemy1_Attack");
		_context.attackController.Attack();

       // _machine.changeState<Enemy_PatrolState>();
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