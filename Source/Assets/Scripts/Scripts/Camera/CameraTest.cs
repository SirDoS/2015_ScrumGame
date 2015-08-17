using UnityEngine;
using System.Collections;

public class CameraTest : MonoBehaviour {
	public Transform target;
	public float cameraSpeed = 10.0f;
	public Vector3 cameraOffset;

	private void LateUpdate(){
		transform.position = Vector3.MoveTowards(transform.position, 
		                                         target.position + cameraOffset, 
		                                         Time.deltaTime * cameraSpeed);
	}
}
