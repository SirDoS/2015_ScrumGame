﻿using UnityEngine;
using System.Collections;

public class VictoryController : MonoBehaviour {

	bool isPlayerInside = false;
    bool isGanhou;

	// Use this for initialization
	void Start () {
        isGanhou = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(isPlayerInside && !isGanhou){
            isGanhou = true;
			Debug.Log("Ganhou");
		}
	}

	void OnTriggerEnter2D(Collider2D ganhador){
		PandaController pandaController = ganhador.gameObject.GetComponent<PandaController>();
		if(pandaController != null){
			isPlayerInside = true;
		}
	}
}
