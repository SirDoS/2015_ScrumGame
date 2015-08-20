using UnityEngine;
using System.Collections;

public class BaseChar : BaseActor
{	
	public float horizontalMovementSpeed;
	public float verticalMovementSpeed;

	public PhysicsController physicsController;
	public AnimationController animatorController;

	public override void Damage (int pDamage)
	{
		base.Damage (pDamage);
	}
}