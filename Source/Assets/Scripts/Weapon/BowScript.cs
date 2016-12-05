using UnityEngine;
using System.Collections;

public class BowScript : BaseWeapon {

	bool alreadyHitted = false;

	void Update(){
		if(!alreadyHitted){
			alreadyHitted = true;
			Attack();
		}
	}

	public override void Attack ()
	{
		base.Attack ();

		foreach(RaycastHit2D hit in GetEnemiesAhead()){
			//Debug.Log(hit.transform.name);
			BaseActor actor = hit.transform.GetComponent<BaseActor>();
			DoDamage(actor);
		}
	}

	public override void DoDamage (BaseActor pTarget)
	{
		//Debug.Log("Katana Do Damage");

		base.DoDamage (pTarget);
	}
}