using UnityEngine;
using System.Collections;

public class BaseChar : BaseActor
{	
	public float horizontalMovementSpeed;
	public float verticalMovementSpeed;

	public GameplayController gameplayController;
	public PhysicsController physicsController;
	public AnimationController animatorController;

	public void doDamage (float pDamage, GameObject pCauser)
	{
		
	}

	public void receiveDamage(){

	}
}