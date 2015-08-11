using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_JumpState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		
		//Tocar animacao IDLE.
		//Parar o personagem.
	}
	
	public override void end ()
	{
		base.end ();
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		
	}
	#endregion
}
