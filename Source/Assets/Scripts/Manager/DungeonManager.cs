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

	public static DungeonManager Instance{
		get{
			if(instance == null){
				instance = FindObjectOfType(typeof(DungeonManager)) as DungeonManager;
			}
			return instance;
		}
	}

	void Start(){
		CreateDungeonTile(Vector3.zero);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.P)){
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
	
}
