using UnityEngine;
using System.Collections;

public class DungeonTile : MonoBehaviour, IPoolObject{

	public Transform entrance;
	public Transform exit;

	private SpawnPool myPool;


	void Start () {
		//DungeonManager.Instance.AddDungeonTiles(this);
	}

	#region IPoolObject implementation
	public void OnSpawn (SpawnPool pMyPool)
	{
		Debug.Log("OnSpawn, apenas");
		myPool = pMyPool;
	}
	public void Despawn ()
	{
		Debug.Log("Despawn, apenas", this);
		myPool.Despawn(this.gameObject);
	}
	public void DespawnIn (float fDelay)
	{
	}
	public void OnDespawn ()
	{
	}
	#endregion
}
