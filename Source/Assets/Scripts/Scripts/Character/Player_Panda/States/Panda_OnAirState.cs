using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Panda_OnAirState : SKMecanimState<PandaController>{
	public override void begin ()
	{
		base.begin ();
	}
	
	public override void reason ()
	{
		base.reason ();

		float vertical = Input.GetAxis("Vertical");

		if(vertical == 0.0f){
			_machine.changeState<Panda_LandState>();
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
