using UnityEngine;
using System.Collections;

public class SwordScript : BaseWeapon {

	public override void Attack ()
	{
		base.Attack ();

		//Debug.Log("Sword Do Damage");

		foreach(RaycastHit2D hit in GetEnemiesAhead()){
			//Debug.Log(hit.transform.name);
			BaseActor actor = hit.transform.GetComponent<BaseActor>();
			DoDamage(actor);
		}
	}

	public override void DoDamage (BaseActor pTarget)
	{
		//Debug.Log("Sword Do Damage");

		base.DoDamage (pTarget);
	}
}
