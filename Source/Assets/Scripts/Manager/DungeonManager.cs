using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DungeonManager : MonoBehaviour {

	private static DungeonManager instance;
	public List<DungeonTile> dungeonTiles = new List<DungeonTile>();
	public List<SpawnPool> tilesPools = new List<SpawnPool>();

	[Header("Tiles Iniciais")]
	public List<SpawnPool> startTilePools = new List<SpawnPool>();
	[Header("Tiles Finais")]
	public List<SpawnPool> endTilePools = new List<SpawnPool>();
	
	public const int MAX_TILES = 4;
	public const int INITIAL_TILES = 2;

	public int theFinalCountDown = 5;
	public bool gameOver;

	public static DungeonManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType(typeof(DungeonManager)) as DungeonManager;
			}
			return instance;
		}
	}

	void Start(){
		CreateStartDungeonTile(Vector3.zero);
		for(int i = 0; i < INITIAL_TILES; i++){
			CreateDungeonTile(dungeonTiles[dungeonTiles.Count - 1].exit.position);
		}
	}

	public void AddDungeonTiles(DungeonTile pDungeonTiles){
		dungeonTiles.Add(pDungeonTiles);				
	}

	public void RemoveFirstDungeonTile(){
		if(dungeonTiles.Count > 0){
			dungeonTiles[0].Despawn();
			dungeonTiles.RemoveAt(0);
		}else{
			Debug.LogError("Remove Dungeon Tile, deu ruim, viado");
		}
	}
	
	public void CreateDungeonTile(Vector3 pPosition){
		Debug.Log ("CreatingDungeonTile");
		int i = Random.Range(0, tilesPools.Count);
		DungeonTile tile = tilesPools[i].Spawn<DungeonTile>(pPosition, Quaternion.identity);

		tile.transform.position -= tile.entrance.localPosition;

		AddDungeonTiles(tile);

		if(dungeonTiles.Count > MAX_TILES){
			RemoveFirstDungeonTile();
		}
	}

	public void CreateStartDungeonTile(Vector3 pPosition){
		Debug.Log ("CreatingStartDungeonTile");
		int i = Random.Range (0, startTilePools.Count);
		DungeonTile tile = startTilePools[i].Spawn<DungeonTile>(pPosition, Quaternion.identity);

		//tile.transform.position -= tile.entrance.localPosition;

		AddDungeonTiles(tile);

		if(dungeonTiles.Count > MAX_TILES){
			RemoveFirstDungeonTile();
		}
	}

	public void CreateEndDungeonTile(Vector3 pPosition){
		Debug.Log ("CreatingEndDungeonTile");
		int i = Random.Range (0, endTilePools.Count);
		DungeonTile tile = endTilePools[i].Spawn<DungeonTile>(pPosition, Quaternion.identity);
		
		tile.transform.position -= tile.entrance.localPosition;
		
		AddDungeonTiles(tile);
		
		if(dungeonTiles.Count > MAX_TILES){
			RemoveFirstDungeonTile();
		}
	}

	/*
	 * Quando entra no trigger!
	 */
	public void MessageInAEnter(){
		if(theFinalCountDown == 0 && !gameOver){
			gameOver = true;
			CreateEndDungeonTile(dungeonTiles[dungeonTiles.Count - 1].exit.position);
		}else if (!gameOver){
			CreateDungeonTile(dungeonTiles[dungeonTiles.Count - 1].exit.position);
			theFinalCountDown--;
		}
	}

}
