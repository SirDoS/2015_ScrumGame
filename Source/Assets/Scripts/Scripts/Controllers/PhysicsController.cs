using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicsController : MonoBehaviour {

	public bool isGrounded;

	public float skinWidth = 0.05f;

	public LayerMask groundLayers;

	private Rigidbody2D cachedRigidbody;
	private Collider2D cachedCollider;

	void Awake(){
		if(cachedRigidbody == null){
			cachedRigidbody = GetComponent<Rigidbody2D>();
		}
		if(cachedCollider == null){
			cachedCollider = GetComponent<Collider2D>();
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
		RaycastHit2D hit = Physics2D.Raycast(cachedRigidbody.position, Vector2.down,
		                                     (cachedCollider.bounds.size.y/2) + skinWidth, groundLayers.value);
		if(hit.transform != null)
			return true;
		else
			return false;

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