using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_LandState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		
		//Tocar animacao LAND.
		//Parar o personagem.
	}
	
	public override void reason ()
	{
		base.reason ();
		
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		if(vertical < 0.3f && horizontal == 0.0f){
			_machine.changeState<Girl_IdleState>();
			return;
		}if(vertical < 0.3f && horizontal != 0){
			_machine.changeState<Girl_RunState>();
			return;
		}/*if(vertical > 0){
			_machine.changeState<Girl_JumpState>();
			return;
		}*/

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
