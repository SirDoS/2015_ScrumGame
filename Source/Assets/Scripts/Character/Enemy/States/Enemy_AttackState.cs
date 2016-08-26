using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_AttackState : SKMecanimState<EnemyController> {
	float time;

	public override void begin()
	{
		time = 0;
		base.begin ();
		_context.lineOfAttack.onTriggerExitCallback += OnTargetExit;
		_context.lineOfSight.onTriggerExitCallback += OnTargetExitSight;

		_context.animatorController.PlayState("Enemy1_Attack");
		_context.attackController.Attack();
	}
	
	public override void reason()
	{
		base.reason ();
		if(time < 0.72){
			_context.lineOfAttack.onTriggerStayCallback += OnTargetStay;
		}else{
			time = 0;
			_machine.changeState<Enemy_OnChaseState>();
		}
	}
	
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		time += Time.deltaTime;
	}
	
	public override void end()
	{
		base.end ();
	}

	public void OnTargetExitSight (Collider2D pCollider) {
		if(pCollider.CompareTag("Player")) {
			//Debug.Log (pCollider.name);
			BaseActor actor = pCollider.GetComponent<BaseActor>();

			if (actor == _context.iaController.iaTarget) {
				_machine.changeState<Enemy_PatrolState>();
			}
		}
	}

	public void OnTargetExit (Collider2D pCollider) {
		if(pCollider.CompareTag("Player")) {
			//Debug.Log (pCollider.name);
			BaseActor actor = pCollider.GetComponent<BaseActor>();

			if (actor == _context.iaController.iaTarget) {
				_machine.changeState<Enemy_OnChaseState>();
			}
		}

	}

	public void OnTargetStay (Collider2D pCollider) {
		if(pCollider.CompareTag("Player")) {
			BaseActor actor = pCollider.GetComponent<BaseActor>();

			if (actor == _context.iaController.iaTarget) {
				_context.animatorController.PlayState("Enemy1_Attack");

				if(time == .5){
					_context.attackController.Attack();
				}else{
					_context.physicsController.SetVelocity(new Vector2(0,0));
					time = 0;
				}
			}
		}
	}

}