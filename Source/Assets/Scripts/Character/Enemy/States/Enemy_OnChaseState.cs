using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_OnChaseState : SKMecanimState<EnemyController> {

	float time;

	public override void begin()
	{
		time = 0;
		base.begin ();
		_context.lineOfAttack.onTriggerEnterCallback += OnTargetEnter;

		_context.physicsController.SetVelocity(Vector2.zero);
		_context.animatorController.PlayState("Enemy1_Walk");
	}

	public override void reason()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded())
		{

		}
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		time += Time.deltaTime;
	}

	public void OnTargetEnter(Collider2D pTarget){
		if(pTarget.CompareTag("Player")){
			Debug.Log(pTarget.name);
			_machine.changeState<Enemy_AttackState>();
		}
	}

		public override void end()
	{
		base.end ();
		_context.lineOfAttack.onTriggerEnterCallback -= OnTargetEnter;
	}
	
}
