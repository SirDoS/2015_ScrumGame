using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class EnemyController : BaseChar 
{

	public SKMecanimStateMachine<EnemyController> enemyStateMachine;

	// Use this for initialization
	void Start () 
	{
		currentLife = maxLife;
		isAlive = true;

		enemyStateMachine = new SKMecanimStateMachine<EnemyController>(animatorController.Animator, this, new Enemy_IdleState());
		enemyStateMachine.addState(new Enemy_IdleState());
		enemyStateMachine.addState(new Enemy_WalkState());
		enemyStateMachine.addState(new Enemy_OnHitState());
		enemyStateMachine.addState(new Enemy_AttackState());
	}
	
	// Update is called once per frame
	void Update () 
	{
		enemyStateMachine.update(Time.deltaTime);	
	}
}
