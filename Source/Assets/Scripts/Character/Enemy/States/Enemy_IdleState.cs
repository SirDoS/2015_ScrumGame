using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_IdleState : SKMecanimState<EnemyController> {

	float time;

	public override void begin()
	{
		bool landed = false;
		time = 0;
		base.begin ();
		_context.lineOfSight.onTriggerEnterCallback += OnTargetEnter;

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

		if(_context.physicsController.IsGrounded())
		{
			if(time >= 2.0f){
				_machine.changeState<Enemy_PatrolState>();
			}
		}
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		time += Time.deltaTime;
	}

	public void OnTargetEnter(Collider2D pTarget){
		if(pTarget.CompareTag("Player")){
			Debug.Log(pTarget.name);
			_context.iaController.iaTarget = pTarget.GetComponent<BaseActor>();
			_machine.changeState<Enemy_OnChaseState>();
		}
	}

	public override void end()
	{
		base.end ();
		_context.lineOfSight.onTriggerEnterCallback -= OnTargetEnter;
	}
	
}
