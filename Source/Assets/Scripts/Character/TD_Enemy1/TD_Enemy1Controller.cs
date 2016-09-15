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

	/// <summary>
	/// A value indicating how much the enemy walked, it's not the real amount
	/// <para /> walked, but a value which the higer the value, the closer to the goal.
	/// </summary>
	/// <returns>The to goal.</returns>
	public float distanceToGoal() {
		float distance = Vector3.Distance(startPosition, gameObject.transform.position);
		return F_function (distance);
		return distance;
	}

	/// <summary>
	/// <para />Retorna f(_x) onde é garantido que o valor retornado estara entre currentCheckpoint - 1 até currentcheckpoint.
	/// <para />, resumidamente descreve uma função que sera alterada conforme o checkpoint para medir as distancias
	/// <para />garantindo que a distancia de um enemy que esteja em um checkpoint mais proximo do fim sempre estara na frente.
	/// <para />Duvidas verificar: http://www.wolframalpha.com/input/?i=(x%5E2+%2B+0)%2F(x%5E2+%2B+1)
	/// <para />http://www.wolframalpha.com/input/?i=(2x%5E2+%2B+1)%2F(x%5E2+%2B+1)
	/// <para />http://www.wolframalpha.com/input/?i=(4x%5E2+%2B+3)+%2F(x%5E2+%2B+1)
	/// </summary>
	/// <returns>The function.</returns>
	/// <param name="_x">X.</param>
	private float F_function(float _x){
		return ((currentWaypoint * (Mathf.Pow ((_x), 2)) + (currentWaypoint - 1))
			/ (Mathf.Pow((_x),2) + 1)); 
	}

	#region IWalker implementation

	public float speed = 0.01f;

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
		/*
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
		*/
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

	/// <summary>
	/// Destroys this instance.
	/// </summary>
	/// <returns>true</returns>
	/// <c>false</c>
	public bool destroyMe ()
	{
		Destroy (gameObject);
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
