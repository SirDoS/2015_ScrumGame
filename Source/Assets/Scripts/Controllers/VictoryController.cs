using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour {

	bool isPlayerInside = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInside){
			Debug.Log("GANHOU FILHO DA PUTA!!!");
		}
	}

	void OnTriggerEnter2D(Collider2D ganhador){
		GirlController girlController = ganhador.gameObject.GetComponent<GirlController>();
		if(girlController != null){
			isPlayerInside = true;
		}
	}
}
