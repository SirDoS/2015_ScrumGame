using UnityEngine;
using System.Collections;

public class CameraFocus : MonoBehaviour {

	/// <summary>
	/// Quantia de pixels que "trigeram" o movimento
	/// </summary>
	public float mDelta = 10f;
	/// <summary>
	/// Velocidade do movimento
	/// </summary>
	public float mSpeed = 5.0f;


	private static float upperEdge = 10;
	public static float UpperEdge 
	{
		get
		{ 
			return upperEdge;
		}
		set
		{
			upperEdge = value;
		}
	}

	private Vector3 mRightDirection = Vector3.right; // Direction the camera should move when on the right edge
	private float cameraSize;
	// Use this for initialization
	void Start () {
		cameraSize = GetComponent<Camera> ().orthographicSize;
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.mousePosition.x >= Screen.width - mDelta )
		{
			//Debug.Log ("Living on the edge!" + Screen.width + " OtherVar: " + Screen.height);
			//Debug.Log ("Mouse position X: " + Input.mousePosition.x + " y: " + Input.mousePosition.y);
			// Move the camera
			if (transform.position.x + cameraSize < UpperEdge ) {
				transform.position += Vector3.right * Time.deltaTime * mSpeed;
			} else {
				//Debug.Log ("saindo demais do mapa para direita");
			}
		}
		if ( Input.mousePosition.y >= Screen.height - mDelta )
		{
			if (transform.position.y + cameraSize < UpperEdge + 0.5f ) {
				//Debug.Log ("CameraSize: " + cameraSize  +" UE: " + UpperEdge);
				transform.position += Vector3.up * Time.deltaTime * mSpeed;
			} else {
				//Debug.Log ("saindo demais do mapa para cima");
			}
		}
		if (Input.mousePosition.x <= mDelta) {
			if (transform.position.x > 0 + mSpeed) {
				transform.position += Vector3.left * Time.deltaTime * mSpeed;
			} else {
				//Debug.Log ("Saindo demais do mapa para esquerda!");
			}
		}
		if ( Input.mousePosition.y <= mDelta )
		{
			if (transform.position.y > 0 + mSpeed) {
				transform.position += Vector3.down * Time.deltaTime * mSpeed;
			} else {
				//Debug.Log ("Saindo demais do mapa para baixo!");
			}
		}
	}


	/*
	function Update ()
	{
		// Check if on the right edge
		if ( Input.mousePosition.x >= Screen.width - mDelta )
		{
			// Move the camera
			transform.position += mRightDirection * Time.deltaTime * mSpeed;
		}
	}
	*/
}
