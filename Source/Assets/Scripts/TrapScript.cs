using UnityEngine;
using System.Collections;

public class TrapScript : BaseWeapon {

	bool isPlayerInside = false;
	int weaponDamage = 12;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInside){
			DoDamage();													
			}
	}

	void OnTriggerEnter2D(Collider2D ganhador){
		GirlController girlController = ganhador.gameObject.GetComponent<GirlController>();
		if(girlController != null){
			isPlayerInside = true;
		}
	}
}
