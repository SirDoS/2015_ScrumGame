using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CenaManager : MonoBehaviour {


	private CenaManager (){
	}
	static CenaManager(){
		
	}

	public static CenaManager Instance = new CenaManager ();

	public void mudarCena(string nome) {
		SceneManager sm;
		Scene esta = SceneManager.GetActiveScene ();
		Debug.Log ("Name: " + esta.name);
		Debug.Log ("Name: " + esta.buildIndex);
		//SceneManager.UnloadScene (esta);
		Scene prox = SceneManager.CreateScene (nome);
		SceneManager.SetActiveScene (prox);
		SceneManager.LoadScene (nome);
		Debug.Log ("Name: " + prox.name);
		Debug.Log ("Index: " + prox.buildIndex);
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
