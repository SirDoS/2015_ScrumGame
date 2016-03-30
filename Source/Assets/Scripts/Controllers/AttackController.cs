using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WEAPON_ID{
	// Pode ser passado um valor diferente, default = 0, e incrementa de 1 em 1;
	KATANA,
	SWORD,
	BAMBU,
	CHAVEDEFENDA
}

public class AttackController : MonoBehaviour {

	public BaseWeapon equippedWeapon;
	public List <BaseWeapon> weaponsList ;

	public void Update(){
        if (Input.GetKeyDown(KeyCode.F1)){
			SelectWeapon(WEAPON_ID.SWORD);
		}else if(Input.GetKeyDown(KeyCode.F2)){
			SelectWeapon(WEAPON_ID.KATANA);
		}
	}

	public void Attack()
	{
        equippedWeapon.Attack();        
	}

	/*public void Teste(){
		weaponsList.Add(KatanaScript);
		weaponsList.Add(SwordScript);


		SelectWeapon(WEAPON_ID.BAMBU);
		Debug.Log(equippedWeapon.ToString());

		
		SelectWeapon(WEAPON_ID.SWORD);
		Debug.Log(equippedWeapon.ToString());

	}*/

	public void SelectWeapon(WEAPON_ID pID){
		for(int i = 0; i < weaponsList.Count; i++){
			if(weaponsList[i].weaponID == pID){
				equippedWeapon = weaponsList[i];
			}
		}
	}
}

