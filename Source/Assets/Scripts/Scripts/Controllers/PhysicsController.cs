using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsController : MonoBehaviour {

	public bool isGrounded;

	private Rigidbody cachedRigidbody;

	void Awake(){
		if(cachedRigidbody == null){
			cachedRigidbody = GetComponent<Rigidbody>();
		}
	}

	public Rigidbody CachedRigidbody {
		get {
			return cachedRigidbody;
		}
		set {
			cachedRigidbody = value;
		}
	}

	public bool IsGrounded() {
		return true;
	}

	public void SetVelocity(Vector3 pVeloctiy){
		cachedRigidbody.velocity = pVeloctiy;
	}
	public Vector3 GetVelocity(){
		return cachedRigidbody.velocity;
	}

	public void AddForce(Vector3 pDirection, float pForce, ForceMode pMode = ForceMode.Force){
		cachedRigidbody.AddForce(pDirection * pForce, pMode);
	}

}