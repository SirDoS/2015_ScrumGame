using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour {

	public bool isGrounded;

	private Rigidbody2D cachedRigidbody;

	void Awake(){
		if(cachedRigidbody == null){
			cachedRigidbody = GetComponent<Rigidbody2D>();
		}
	}

	public Rigidbody2D CachedRigidbody {
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

	public void SetVelocity(Vector2 pVeloctiy){
		cachedRigidbody.velocity = pVeloctiy;
	}
	public Vector2 GetVelocity(){
		return cachedRigidbody.velocity;
	}

	public void AddForce(Vector2 pDirection, float pForce){
		cachedRigidbody.AddForce(pDirection * pForce);
	}

}