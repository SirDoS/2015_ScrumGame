using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseTrap : MonoBehaviour, IPoolObject
{
	public TRAP_ID trapID;

	protected int tickCounter;
	protected int maxTicks = 4;

    public int trapDamage;
	public float damageCooldown = 2f;
	protected float lastDamageTime;

	public List<BaseActor> actorsInside;

	private SpawnPool myPool;

	protected virtual void Update(){
		lastDamageTime += Time.deltaTime;

	}

	void OnTriggerEnter2D(Collider2D pMorredor){
		BaseActor actor = pMorredor.gameObject.GetComponent<BaseActor>();
		if (actor != null)
		{
			DoDamage(actor);
			if(!actorsInside.Contains(actor))
				actorsInside.Add(actor);
		}
	}

	void OnTriggerExit2D(Collider2D pMorredor){
		BaseActor actor = pMorredor.gameObject.GetComponent<BaseActor>();
		if (actor != null)
		{
			if(actorsInside.Contains(actor))
				actorsInside.Remove(actor);
		}
	}

	public virtual void DoDamage(BaseActor pTarget){
		if(pTarget != null){
			if((tickCounter < maxTicks) || maxTicks <= 0){
				pTarget.DoDamage(trapDamage, null);
				tickCounter++;
			}
		}
	}

	#region IPoolObject implementation
	public void OnSpawn (SpawnPool pMyPool)
	{
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
