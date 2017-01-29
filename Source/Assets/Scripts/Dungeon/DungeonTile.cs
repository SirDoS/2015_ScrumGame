using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DungeonTile : MonoBehaviour, IPoolObject{


    /// <summary>
    /// Indica que o objeto foi spawnado.
    /// </summary>
    //public EventHandler<EventArgs> Spawned;

	public Transform entrance;
	public Transform exit;

	private SpawnPool myPool;

	public bool alreadyTriggered;

    public float Width = 0;
    public int resourceAmount = 6;


    /// <summary>
    /// Locais onde podem ser spawnados recursos
    /// </summary>
    public List<Vector3> ResourcePoints = new List<Vector3>();

    void Start () {
        BoxCollider2D  bd= this.gameObject.GetComponent<BoxCollider2D>();
	}
    private void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DungeonManager.Instance.onEnterTile(this.transform);
        }
    }


    #region IPoolObject implementation
    public void OnSpawn (SpawnPool pMyPool)
	{
		//Debug.Log("OnSpawn, apenas");
		myPool = pMyPool;
	}
	public void Despawn ()
	{
		//Debug.Log("Despawn, apenas", this);
		myPool.Despawn(this.gameObject);
		alreadyTriggered = false;
	}
	public void DespawnIn (float fDelay)
	{
	}
	public void OnDespawn ()
	{
	}
	#endregion


}
