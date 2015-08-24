using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackOnAirState : SKMecanimState<GirlController>
{
	float timeOnState;

	// Use this for initialization
	public override void begin() {
		base.begin();
		timeOnState = 0;
		Debug.Log("Im Attacking OnAir, biatch!");
		//_machine.animator.Play("AttackOnAir");
	}

	public override void reason (){

		base.reason();
		if(timeOnState > 0.3f){
			_machine.changeState<Girl_OnAirState>();
		}
	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
	}
	#endregion

	public override void end (){

		base.end();
	}
}
