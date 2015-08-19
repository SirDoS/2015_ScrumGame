using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_OnAirState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		float vertical = Input.GetAxis("Vertical");
/*		float horizontal = Input.GetAxis("Horizontal");*/

		if(vertical <0.5f){
			_machine.changeState<Girl_LandState>();
			return;
		}/*if(horizontal > 0){

		}*/
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
}
