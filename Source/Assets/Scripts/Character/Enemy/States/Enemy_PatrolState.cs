using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Enemy_PatrolState : SKMecanimState<EnemyController> {
	
	public override void begin()
	{
		base.begin ();

		_context.animatorController.PlayState("Enemy1_Walk");
	}
	
	public override void reason()
	{
		base.reason ();

		RaycastHit2D hit = Physics2D.Raycast(_context.transform.position, _context.transform.forward, 1.0f);

		if(hit.transform != null){
			Debug.Log(hit.transform.name);
		}
	}
	
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	
	public override void end()
	{
		base.end ();
	}

}