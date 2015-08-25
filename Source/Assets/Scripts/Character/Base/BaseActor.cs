using UnityEngine;
using System.Collections;

public class BaseActor : MonoBehaviour
{
	private Transform cachedTransform;

	[SerializeField] 
	protected bool isAlive = true;
	protected int maxLife = 100;


	public int currentLife = 100;

	public Vector3 Position{
		get {
			if(cachedTransform == null){
				cachedTransform = transform;
			}
			return cachedTransform.position;
		}
				
		
	}

	public virtual void Damage(int pDamage)
	{

	}
}