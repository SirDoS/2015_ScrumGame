using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlaceTower : MonoBehaviour {


	public GameObject monsterPrefab;
	public GameObject monster;

	public bool canPlaceTower() {
		int cost = monsterPrefab.GetComponent<TowerData> ().levels[0].cost;
		return monster == null && TowerDefenseManager.Instance.Gold >= cost;
	}

	void Update(){
	}

	void FixedUpdate() {
		/*RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 20f, 3);
		if (hit.collider != null) {
			float distance = Mathf.Abs(hit.point.y - transform.position.y);
			float heightError = floatHeight - distance;
			float force = liftForce * heightError - rb2D.velocity.y * damping;
			rb2D.AddForce(Vector3.up * force);
		}*/
	}
	

	/// <summary>
	/// Raises the mouse up event.
	/// </summary>
	public void OnMouseUp () {
		Debug.Log ("Ta clicando ao menos");
		if (canPlaceTower ()) {
			Vector3 inspos = new Vector3 (transform.position.x, transform.position.y, transform.position.z - 1);
			monster = (GameObject)
				Instantiate (monsterPrefab, inspos , Quaternion.identity);
			//AudioSource audioSource =  gameObject.GetComponent<AudioSource>();
			//audioSource.PlayOneShot(audioSource.clip);
			//TODO : DEDUCT GOLD
			TowerDefenseManager.Instance.Gold -= monster.GetComponent<TowerData> ().CurrentLevel.cost;
		} else if (canUpgradeTower ()) {
			monster.GetComponent<TowerData> ().increaseLevel ();
			//AudioSource audioSource = gameObject.GetComponent<AudioSource>();
			//audioSource.PlayOneShot(audioSource.clip);
			TowerDefenseManager.Instance.Gold -= monster.GetComponent<TowerData> ().CurrentLevel.cost;
			// TODO: Deduct gold
		}
	}



	private bool canUpgradeTower() {
		if (monster != null) {
			TowerData monsterData = monster.GetComponent<TowerData> ();
			TowerLevel nextLevel = monsterData.getNextLevel();
			if ((nextLevel != null)) {
				int cost = nextLevel.cost;
				if (TowerDefenseManager.Instance.Gold >= cost){
					return true;
				}
			}
		}
		return false;
	}

	// Update is called once per frame
	void Start () {
		
	}
}
