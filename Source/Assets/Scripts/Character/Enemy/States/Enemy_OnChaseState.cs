using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_OnChaseState : SKMecanimState<EnemyController> {

	float time;
	Vector3 targetPosition;

	public override void begin()
	{
		time = 0;
		base.begin ();
		_context.lineOfSight.onTriggerExitCallback += OnTargetExit;
		_context.lineOfAttack.onTriggerEnterCallback += OnTargetEnterAttack;
		_context.animatorController.PlayState("Enemy1_Walk");
	}

	public override void reason()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded())
		{
			targetPosition  = _context.iaController.iaTarget.Position;
		}
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		time += Time.deltaTime;

		Chase(targetPosition);
	}

		public override void end()
	{
		base.end ();

		_context.lineOfSight.onTriggerExitCallback -= OnTargetExit;
		_context.lineOfAttack.onTriggerEnterCallback -= OnTargetEnterAttack;
	}

	public void Chase (Vector3 pTargetPosition){
		Vector3 direction = pTargetPosition - _context.Position;
		Vector3 scale = _context.transform.localScale;
		direction.Normalize();

		if (direction.x > 0.0f) {
//			scale.x = -1;
//			_context.physicsController.SetScale (scale);
			_context.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
		} else if (direction.x < 0.0f) {
//			scale.x = 1;
//			_context.physicsController.SetScale (scale);
			_context.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}

		_context.physicsController.SetVelocity(new Vector2(direction.x, _context.physicsController.GetVelocity().y));
	}

	public void OnTargetEnterAttack(Collider2D pTarget){
		if(pTarget.CompareTag("Player")){
			//Debug.Log(pTarget.name);
			BaseActor actor = pTarget.GetComponent<BaseActor>();

			if(actor == _context.iaController.iaTarget){
				_machine.changeState<Enemy_AttackState>();
			}

		}
	}

	public void OnTargetExit (Collider2D pCollider) {
		if(pCollider.CompareTag("Player")) {
			//Debug.Log (pCollider.name);
			BaseActor actor = pCollider.GetComponent<BaseActor>();

			if (actor == _context.iaController.iaTarget) {
				_machine.changeState<Enemy_PatrolState>();
			}
		}

	}
	
}
