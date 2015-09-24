using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseTrap : MonoBehaviour {

	public TRAP_ID trapID;

	public int tickCounter;
	public int maxTicks = 4;

    public int trapDamage;
	public float damageCooldown = 2f;
	protected float lastDamageTime;

	public List<BaseActor> actorsInside;

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
				pTarget.Damage(trapDamage);
				tickCounter++;
			}
		}
	}

}
