using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Classe simples que representa uma wave
/// Nao será adicionada muita logica complexa por motivos de tempo.
/// </summary>
[System.Serializable]
public class Wave {
	public GameObject enemyPrefab;
	public float spawnInterval = 2;
	public int maxEnemies = 20;
	public int roadNumber = -1;
}

/// <summary>
/// Gerencia os inimigos no mundo.
/// </summary>
[System.Serializable]
public class SpawnEnemy  {

	public GameObject[] waypoints;
	public GameObject testEnemyPrefab;

	public Wave[] waves;
	public int timeBetweenWaves = 5;

	private TowerDefenseManager gameManager;

	private float lastSpawnTime;
	private int enemiesSpawned = 0;
	private List<Caminho> paths;

	public SpawnEnemy(List<Caminho> caminhos) {
		paths = caminhos;
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("TowerDefenseManager").GetComponent<TowerDefenseManager>();
	}

	// Use this for initialization
	void Start () {
		lastSpawnTime = Time.time;
		gameManager =
			GameObject.Find("TowerDefenseManager").GetComponent<TowerDefenseManager>();
	}

	// Update is called once per frame
	public void Update () {
		// 1
		int currentWave = gameManager.Wave;
		if (currentWave < waves.Length) {
			// 2
			float timeInterval = Time.time - lastSpawnTime;
			float spawnInterval = waves[currentWave].spawnInterval;
			if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) ||
				timeInterval > spawnInterval) && 
				enemiesSpawned < waves[currentWave].maxEnemies) {
				// 3  
				lastSpawnTime = Time.time;
				GameObject newEnemy = (GameObject)
					GameObject.Instantiate(waves[currentWave].enemyPrefab);
				TD_Enemy1Controller controler =  newEnemy.GetComponent<TD_Enemy1Controller>();
				if (currentWave < (waves.Length / 2)) {
					//Encontra maior caminho disponivel e escolhe ele.
					Caminho biggest = paths[0];
					for (int i = 1; i < paths.Count; i++) {
						if (paths [i].Size > biggest.Size)
							biggest = paths [i];
					}
					biggest.followPath (controler);

				} else {
					paths [Random.Range (0, paths.Count)].followPath (controler);
				}
				//newEnemy.GetComponent<TD_Enemy1Controller>().waypoints = waypoints;
				enemiesSpawned++;
			}
			// 4 
			if (enemiesSpawned == waves[currentWave].maxEnemies &&
				GameObject.FindGameObjectWithTag("Enemy") == null) {
				gameManager.Wave++;
				gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
				enemiesSpawned = 0;
				lastSpawnTime = Time.time;
			}
			// 5 
		} else {
			gameManager.GameOver = true;
			Debug.Log ("YOU WIN");
			//GameObject gameOverText = GameObject.FindGameObjectWithTag ("GameWon");
			//gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
		}	
	}
}
