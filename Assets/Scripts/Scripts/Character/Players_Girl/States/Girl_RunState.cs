using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_RunState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		//Tocar animacao IDLE.
		//Parar o personagem.
 		
	}
	public override void reason ()
	{
		base.reason ();

		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if(horizontal == 0.0f){
			_machine.changeState<Girl_IdleState>();
		return;
		}
		if(vertical > 0){
			_machine.changeState<Girl_JumpState>();
		}
	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		float horizontal = Input.GetAxis("Horizontal");


		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
		                                                   currentVelocity.y));
	}
	#endregion

	public override void end ()
	{
		base.end ();
	}
}
