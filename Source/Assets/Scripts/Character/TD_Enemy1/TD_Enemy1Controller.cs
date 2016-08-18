using UnityEngine;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections;
using Prime31.StateKit;

public class TD_Enemy1Controller : BaseChar ,  IWalker {

	private SKMecanimStateMachine<TD_Enemy1Controller> tD_Enemy1StateMachine;
	public SKMecanimStateMachine<TD_Enemy1Controller> TD_Enemy1StateMachine{
		get{ 
			if (TD_Enemy1StateMachine == null) {
				tD_Enemy1StateMachine = new SKMecanimStateMachine<TD_Enemy1Controller> (animatorController.CachedAnimator, 
																						this, new TD_Enemy1_WalkState ());
			}
			tD_Enemy1StateMachine.addState (new TD_Enemy1_WalkState ());
			return tD_Enemy1StateMachine;
			//enemyStateMachine = new SKMecanimStateMachine<EnemyController>(animatorController.CachedAnimator, 
			//this, new Enemy_IdleState());
		}
	}

	#region IWalker implementation

	public float speed = 0.05f;

	public event System.EventHandler reachedPoint;

	public void Walk (float x, float y)
	{
		targetX = x;
		targetY = y;
		startPosition = gameObject.transform.position;

	}


	void Update(){
		if (targetX != -1f) {
			//Vector3 startPosition = gameObject.transform.position;
			Vector3 endPosition = new Vector3 (targetX, targetY);

			float pathLength = Vector3.Distance (startPosition, endPosition);
			float totalTimeForPath = pathLength / speed;
			float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
			gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
			if (gameObject.transform.position.Equals (endPosition)) {
				if (reachedPoint != null) {
					lastWaypointSwitchTime = Time.time;
					//startPosition = gameObject.transform.position;
					reachedPoint (this, System.EventArgs.Empty);
				}

			} 
		}

		if (Input.GetKey (KeyCode.Space)) {
			//if (targetX != -1f) {
				//Vector3 startPosition = gameObject.transform.position;
				Vector3 endPosition = new Vector3 (targetX, targetY);

				float pathLength = Vector3.Distance (startPosition, endPosition);
				float totalTimeForPath = pathLength / speed;
				float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
			gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath / totalTimeForPath * totalTimeForPath);
				if (gameObject.transform.position.Equals (endPosition)) {
					if (reachedPoint != null) {
						reachedPoint (this, System.EventArgs.Empty);
					}

				} 
			//}
		}

	}

	/*
void Update(){
		// 1 
	Vector3 startPosition = waypoints [currentWaypoint].transform.position;
	Vector3 endPosition = waypoints [currentWaypoint + 1].transform.position;
	// 2 
	float pathLength = Vector3.Distance (startPosition, endPosition);
	float totalTimeForPath = pathLength / speed;
	float currentTimeOnPath = Time.time - lastWaypointSwitchTime;
	gameObject.transform.position = Vector3.Lerp (startPosition, endPosition, currentTimeOnPath  / totalTimeForPath);
	// 3 
	if (gameObject.transform.position.Equals(endPosition)) {
	  if (currentWaypoint < waypoints.Length - 2) {
		// 3.a 
		currentWaypoint++;
		lastWaypointSwitchTime = Time.time;
		RotateIntoMoveDirection();
	  } else {
		// 3.b 
		Destroy(gameObject);
	 
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
		GameManagerBehavior gameManager =
		GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
		gameManager.Health -= 1;
	  }
	}

	*/

	void Start(){
		lastWaypointSwitchTime = Time.time;

	}
		

	public void Teleport (float x, float y)
	{
		gameObject.transform.position = new Vector3 (x, y, 0);
	}

	public bool destroyMe ()
	{
		GameObject.Destroy (this);
		return true;
	}

	public int CurrentWaypoint {
		get {
			return currentWaypoint;
		}
		set {
			currentWaypoint = value;
		}
	}

	/*
	DynamicMethod squareIt = new DynamicMethod(
		"SquareIt", 
		typeof(long), 
		methodArgs, 
		typeof(Example).Module);
	*/
	private float targetX = -1f;
	private float targetY = -1f;
	private int currentWaypoint = 0;
	private float lastWaypointSwitchTime;// = 0f;
	private Vector3 startPosition;
	//private delegate void SquareItInvoker(int input);
	#endregion

}
