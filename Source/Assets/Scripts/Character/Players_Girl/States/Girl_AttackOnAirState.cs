using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackOnAirState : SKMecanimState<GirlController>
{
	// Use this for initialization
	public override void begin() {
		base.begin();
		Debug.Log("Im Attacking OnAir, biatch!");
		//_machine.animator.Play("AttackOnAir");
	}

	public override void reason (){

		base.reason();

		_machine.changeState<Girl_OnAirState>();
	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	#endregion

	public override void end (){

		base.end();
	}
}
