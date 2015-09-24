using UnityEngine;
using System.Collections;

public class TurtleTrapScript : BaseTrap {

	protected override void Update(){
		//BASE CHAMA A CLASSE PAI.

		base.Update();

		if(lastDamageTime > damageCooldown){
			if(actorsInside.Count > 0){
				for(int i = 0; i < actorsInside.Count; i++){
					DoDamage(actorsInside[i]);
				}
			}
			lastDamageTime = 0;
		}
	}

	public override void DoDamage (BaseActor pTarget)
	{
		Debug.Log("TRAP DOT PORRA");
		
		base.DoDamage (pTarget);
	}
}
