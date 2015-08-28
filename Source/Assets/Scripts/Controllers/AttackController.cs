using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AttackController : MonoBehaviour {

	public BaseWeapon equippedWeapon;
	List <BaseWeapon> weaponsList;

	public void Attack()
	{
		equippedWeapon.Attack();
	}
}

