using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_RunState : SKMecanimState<ArcherController> {

	public override void begin ()
	{
		base.begin ();

		_context.animatorController.PlayState("Archer_Run");
	}

	public override void reason ()
	{
		base.reason ();

		if(_context.physicsController.IsGrounded()){

			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal == 0.0f){
				_machine.changeState<Archer_IdleState>();
				return;
			}

			Vector3 currentScale = _context.transform.localScale;

			if(horizontal < 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x) * -1;
			}else if (horizontal > 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x);
			}

			_context.physicsController.SetScale(currentScale);

			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Archer_JumpState>();
				return;
			}
			/*
			if(Input.GetKeyDown(KeyCode.Slash)){
				_machine.changeState<Archer_AttackOnRunState>();
				return;
			}
			*/
		}else{
			_machine.changeState<Archer_OnAirState>();
			return;
		}
	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal");

		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed
			, currentVelocity.y));
	}

	public override void end ()
	{
		base.end ();
	}
}