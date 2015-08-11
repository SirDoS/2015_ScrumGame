using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsController : MonoBehaviour {

	private Rigidbody cachedRigidbody;

	void Awake(){
		if(cachedRigidbody == null){
			cachedRigidbody = GetComponent<Rigidbody>();
		}
	}

	public void SetVelocity(Vector3 pVeloctiy){
		cachedRigidbody.velocity = pVeloctiy;
	}
	public Vector3 GetVelocity(){
		return cachedRigidbody.velocity;
	}
}