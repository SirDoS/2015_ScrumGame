using UnityEngine;
using System.Collections;

//Classe de base para as armas do jogo
public class BaseWeapon : MonoBehaviour {
	// Quanto de dano tera a arma
	public float damagePoints;
	public GameObject boxCastOrigin;
	public float weaponDamage;

	public Vector2 weaponSize;
	public LayerMask enemiesLayer;
	// Use this for initialization
	void Start () {

	}

	/*Vector3 getBoxCastPosition()
	{
		Vector3 boxPosition;
		boxPosition = boxCastOrigin.transform.position;
		return boxPosition;
	}*/

	public RaycastHit2D[] GetEnemiesAhead(){
		RaycastHit2D[] enemiesSpotted;
		enemiesSpotted = Physics2D.BoxCastAll(boxCastOrigin.transform.position, weaponSize,
		                                      0f, transform.forward, 1f, enemiesLayer.value);
		return enemiesSpotted;

	}

	void DoDamage(BaseActor pTarget){

	}



	// Update is called once per frame
	void Update () {
		//Debug.DrawRay(boxCastOrigin.transform.position, boxCastOrigin.transform.forward, Color.red, 0.5f);
		if(Input.GetKeyDown(KeyCode.F)){
			foreach(RaycastHit2D hit in GetEnemiesAhead()){
				Debug.Log(hit.transform.name);
				BaseActor actor = hit.transform.GetComponent<BaseActor>();
				DoDamage(actor);
			}
		}
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(boxCastOrigin.transform.position, weaponSize);
	}
}
