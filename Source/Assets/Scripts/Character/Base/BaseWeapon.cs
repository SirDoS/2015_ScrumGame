using UnityEngine;
using System.Collections;

//Classe de base para as armas do jogo
public class BaseWeapon : MonoBehaviour 
{
	// Quanto de dano tera a arma
	public GameObject boxCastOrigin;
	public int weaponDamage;

	public Vector2 weaponSize;
	public LayerMask enemiesLayer;

	public RaycastHit2D[] GetEnemiesAhead(){
		RaycastHit2D[] enemiesSpotted;
		enemiesSpotted = Physics2D.BoxCastAll(boxCastOrigin.transform.position, weaponSize,
		                                      0f, transform.forward, 1f, enemiesLayer.value);
		return enemiesSpotted;
	}

	public virtual void DoDamage(BaseActor pTarget){
		if(pTarget != null)
			pTarget.Damage(weaponDamage);
	}

	public virtual void Attack()
	{
		Debug.Log("Ola deus, Attack classe pai!");
	}

	void OnDrawGizmos(){
		if(boxCastOrigin != null)
			Gizmos.DrawWireCube(boxCastOrigin.transform.position, weaponSize);
	}
}
