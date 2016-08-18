using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;




public class TowerDefenseManager : MonoBehaviour {

	/// <summary>
	/// Classe capaz de gerar métodos, todas as configurações devem ser definidas nesta classe.
	/// </summary>
	public MapGenerator generator;


	void Start(){
		generator.generatePath (2, 2, 12, 15, new List<Vector2>());
		//generator.generatePath (22, 19, 2, 16, new List<Vector2>());
		generator.generateTowerLocations();
		generator.generateDecoration ();
	}
		
	void Update(){
		if (Input.GetKey(KeyCode.Space)) {
			GameObject teste =  (GameObject)GameObject.Instantiate (generator.TEMPORARIOENEMY);
				TD_Enemy1Controller control = teste.GetComponent<TD_Enemy1Controller> ();
			if (generator.caminhos.Count > 0) {
				generator.caminhos [0].followPath (control);
				} else {
					Debug.Log ("No caminhos");
				}
		}
		
	}

}
