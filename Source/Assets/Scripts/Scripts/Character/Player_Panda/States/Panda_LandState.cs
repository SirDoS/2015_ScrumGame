using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_LandState : SKMecanimState<PandaController> {
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
		
		if(vertical == 0.0f && horizontal == 0.0f){
			_machine.changeState<Panda_IdleState>();
			return;
		}
		if(vertical == 0.0f && horizontal != 0){
			_machine.changeState<Panda_RunState>();
			return;
		}
		 if(vertical > 0){
			_machine.changeState<Panda_JumpState>();
			return;
		}
		
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
