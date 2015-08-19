using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_JumpState : SKMecanimState<PandaController> {

	//public bool jumped = false;
	public override void begin ()
	{
		base.begin ();
		
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		_context.physicsController.SetVelocity(new Vector2(currentVelocity.x,
		                                                   _context.pandaJumpForce));
		//Tocar animacao JUMP.
		
	}
	
	public override void reason(){
		base.reason ();
		
		/*float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");*/
		
		//Independente de qualquer condicao, passara para OnAirState
		_machine.changeState<Panda_OnAirState>();
		
		
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
}
