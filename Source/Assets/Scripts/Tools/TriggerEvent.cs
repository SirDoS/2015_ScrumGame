using UnityEngine;
using System.Collections;

public class TriggerEvent : MonoBehaviour {

	public delegate void OnTriggerDelegate(Collider2D pCollider); // Lista de metodos que tem como parametro Collider2D
	public OnTriggerDelegate onTriggerEnterCallback;
	public OnTriggerDelegate onTriggerExitCallback;
	
	void OnTriggerEnter2D(Collider2D pCollider){
		if(onTriggerEnterCallback != null){
			Debug.Log("OnTriggerEnter Satanas");
			onTriggerEnterCallback(pCollider);
		}
	}

	void OnTriggerExit2D(Collider2D pCollider){
		if(onTriggerExitCallback != null){
			Debug.Log ("OnTriggerExit , im leaving Tiago");
			onTriggerExitCallback(pCollider);
		}
	}
}
