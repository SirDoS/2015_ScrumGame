using UnityEngine;
using System.Collections;

public class DungeonTile : MonoBehaviour, IPoolObject{

	public Transform entrance;
	public Transform exit;

	private SpawnPool myPool;

	public bool alreadyTriggered;

	public TriggerEvent triggerEntrance;

	void Start () {
		if(triggerEntrance != null){
			triggerEntrance.onTriggerEnterCallback += OnTriggerEntrance;
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

	public void OnTriggerEntrance(Collider2D pCollider){
		//Debug.Log(pCollider.name);
		if(pCollider.CompareTag("Player") && alreadyTriggered == false){
			DungeonManager.Instance.MessageInAEnter();
			alreadyTriggered = true;
		}
	}

}
