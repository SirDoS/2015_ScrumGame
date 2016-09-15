using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_OnAirState : SKMecanimState<ArcherController> {

	public override void begin ()
	{
		base.begin ();

		_context.animatorController.PlayState("Archer_Falling");
	}

	public override void reason ()
	{
		base.reason ();

		float horizontal = Input.GetAxis("Horizontal");

		if(_context.physicsController.IsGrounded())
		{
			_context.gameplayController.enableAirControl = true;
			if(_context.physicsController.GetVelocity().y < 0.5f)
			{
				if(horizontal == 0){
					_machine.changeState<Archer_IdleState>();
					return;
				} else if (horizontal != 0){
					_machine.changeState<Archer_RunState>();
					return;
				}
			}
		}

		if(_context.gameplayController.enableAirControl){
			if (horizontal != 0.0f){
				Vector2 currentVelocity = _context.physicsController.GetVelocity();
				_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
					currentVelocity.y));

				Rotate(horizontal);

			}
		}

		RaycastHit2D wallHit = Physics2D.Raycast(_context.Position, 
			new Vector2(horizontal, 0), 0.3f, 
			1 << 10);
		if(wallHit.transform != null && Input.GetKeyDown(KeyCode.Space)){
			Rotate(-horizontal);
			_machine.changeState<Archer_WallJumpState>();
		}

		/*
		if(Input.GetKeyDown(KeyCode.Slash)){
			_machine.changeState<Archer_AttackOnAirState>();
		}
		*/
	}

	public void Rotate(float pDirection){
		Vector3 currentScale = _context.transform.localScale;

		if(pDirection < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if (pDirection > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * 1;
		}

		_context.transform.localScale = currentScale;
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}

	public override void end ()
	{
		base.end ();
	}
}