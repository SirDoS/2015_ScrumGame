using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TRAP_ID{
	TURTLE,
	PENDULO,
	PLANTA,
	SPIKE
}


public class TrapManager : MonoBehaviour {

	public BaseTrap whichTrap;	
	public List <BaseTrap> trapList ;

	// Use this for initialization
	void Start () {
		
	}
	public void Update(){
		if(Input.GetKeyDown(KeyCode.F1)){
			SelectTrap(TRAP_ID.TURTLE);
		}
	}

	public void SelectTrap(TRAP_ID pID){
		for(int i = 0; i < trapList.Count; i++){
			if(trapList[i].trapID == pID){
				whichTrap = trapList[i];
			}
		}
	}
}
