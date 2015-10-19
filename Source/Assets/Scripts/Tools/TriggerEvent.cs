using UnityEngine;
using System.Collections;

public class TriggerEvent : MonoBehaviour {

	public void OnTriggerEnter2D(Collider2D pCollider){
		Debug.Log (pCollider.name);
	}

	public void OnTriggerExit2D(Collider2D pCollider){
		Debug.Log (pCollider.name);
	}
}
