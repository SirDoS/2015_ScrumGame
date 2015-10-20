using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour {

	public bool alreadyTriggered;

	public TriggerEvent victoryTrigger;

	// Use this for initialization
	void Start () {
		if(victoryTrigger != null){
			victoryTrigger.onTriggerEnterCallback += OnTriggerEntrance;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEntrance(Collider2D pCollider){
		Debug.Log(pCollider.name);
		if(pCollider.CompareTag("Player") && alreadyTriggered == false){
			Debug.Log("Ganhou");
			alreadyTriggered = true;
		}
	}

}
