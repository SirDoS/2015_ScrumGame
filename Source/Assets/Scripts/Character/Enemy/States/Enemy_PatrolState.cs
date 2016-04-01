using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_PatrolState : SKMecanimState<EnemyController> {

	public override void begin()
	{
		base.begin ();
		_context.transform.Rotate(new Vector3(0, 180, 0));
		//_context.physicsController.SetScale (new Vector3(_context.Scale.x * -1, _context.Scale.y, _context.Scale.z));
		_context.animatorController.PlayState("Enemy1_Walk");

		_context.lineOfSight.onTriggerEnterCallback += OnTargetEnter;

		if(_context.physicsController.GetVelocity().x > 0){
//			_context.physicsController.SetScale (new Vector3( 1 , _context.Scale.y, _context.Scale.z));
		}else if(_context.physicsController.GetVelocity().x < 0){
//			_context.physicsController.SetScale (new Vector3( -1, _context.Scale.y, _context.Scale.z));
		}
	}
	
	public override void reason()
	{
		base.reason ();

		_context.physicsController.SetVelocity(new Vector2(-_context.transform.right.x, _context.physicsController.GetVelocity().y));

		//Verificar a parede
		RaycastHit2D hit = Physics2D.Raycast(_context.Position + Vector3.up * 0.3f, -_context.transform.right, 0.50f, _context.patrolPointLayer.value);

		//Debug.DrawRay(_context.Position + Vector3.up * 0.34218f, -_context.transform.right * 0.5f, Color.magenta, 10.0f);	

		if(hit.transform != null){
			_machine.changeState<Enemy_IdleState>();
			return;
		}

		//Verificar o chão
		hit = Physics2D.Raycast(_context.Position + Vector3.up * 0.3f, -_context.transform.right + -_context.transform.up, 1.50f, 1 << 21);

		//Debug.DrawRay(_context.Position + Vector3.up * 0.34218f, (-_context.transform.right + -_context.transform.up) * 1.5f, Color.magenta, 10.0f);	

		if(hit.transform == null){
			_machine.changeState<Enemy_IdleState>();
			return;
		}

		/*//Verificar o ataque
		hit = Physics2D.Raycast(_context.Position + Vector3.up * 0.3f, -_context.transform.right, 0.5f, 1 << 8);

		//Debug.DrawRay(_context.Position + Vector3.up * 0.34218f, -_context.transform.right * 0.5f, Color.magenta, 10.0f);	
		
		if(hit.transform != null){
			_machine.changeState<Enemy_AttackState>();
			return;
		}*/

	}

	public void OnTargetEnter(Collider2D pTarget){
		if(pTarget.CompareTag("Player")){
			//Debug.Log(pTarget.name);
			_context.iaController.iaTarget = pTarget.GetComponent<BaseActor>();
			_machine.changeState<Enemy_AttackState>();
		}
	}
	
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	
	public override void end()
	{
		base.end ();

		_context.lineOfSight.onTriggerEnterCallback -= OnTargetEnter;
	}

}