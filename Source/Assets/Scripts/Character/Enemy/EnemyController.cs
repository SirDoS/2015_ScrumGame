using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class EnemyController : BaseChar, IPoolObject
{
	private SpawnPool myPool;
	private SKMecanimStateMachine<EnemyController> enemyStateMachine;

	public TriggerEvent lineOfSight;
	public TriggerEvent lineOfAttack;

	public LayerMask patrolPointLayer;

	public SKMecanimStateMachine<EnemyController> EnemyStateMachine {
		get {
			if(enemyStateMachine == null){
				enemyStateMachine = new SKMecanimStateMachine<EnemyController>(animatorController.CachedAnimator, 
				                                                               this, new Enemy_IdleState());
				enemyStateMachine.addState(new Enemy_IdleState());
				enemyStateMachine.addState(new Enemy_PatrolState());
				enemyStateMachine.addState(new Enemy_OnHitState());
				enemyStateMachine.addState(new Enemy_AttackState());
				enemyStateMachine.addState(new Enemy_OnDeathState());
				enemyStateMachine.addState(new Enemy_OnChaseState());
			}
			return enemyStateMachine;
		}
	}

	void Update(){
		EnemyStateMachine.update(Time.deltaTime);

		if(!isAlive)
			EnemyStateMachine.changeState<Enemy_OnHitState>();
	}

	public override void OnDeath (int pDamage, BaseActor pKiller){
		base.OnDeath (pDamage, pKiller);

		Despawn();
	}

	public override void Reset(){
		base.Reset();
		EnemyStateMachine.changeState<Enemy_IdleState>();
	}

	#region IPoolObject implementation
	
		public void OnSpawn (SpawnPool pMyPool)
	{
		Reset();

		myPool = pMyPool;
	}

	public void Despawn ()
	{
		myPool.Despawn(gameObject);
	}

	public void DespawnIn (float fDelay)
	{

	}

	public void OnDespawn ()
	{

	}

	#endregion
}