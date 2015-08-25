﻿using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_WallJumpState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		_context.gameplayController.enableAirControl = false;

		float horizontal = Input.GetAxis("Horizontal");

		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.AddForce(new Vector2(currentVelocity.x,
		                                                (Mathf.Abs(horizontal)* -1) * _context.girlJumpForce), -75f);


		//_machine.animator.Play("WallJump");

		_machine.changeState<Girl_OnAirState>();
	}
	public override void reason ()
	{
		base.reason ();
		
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{

	}
	#endregion

	public override void end ()
	{
		base.end ();
	}

	public void Rotate(float pDirection){
		Vector3 currentScale = _context.transform.localScale;
		
		if(pDirection < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if(pDirection > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * 1;
		}
		_context.transform.localScale = currentScale;
	}

}