using UnityEngine;
using System.Collections;

public class EnemySpawnerTest : MonoBehaviour {

	public SpawnPool enemyPools;

	void Update () {
		if(Input.GetKeyDown(KeyCode.P)){
			enemyPools.Spawn<BaseActor>(new Vector3(6.38f, 1.22f, 0f), Quaternion.identity);
		}		
	}
}
