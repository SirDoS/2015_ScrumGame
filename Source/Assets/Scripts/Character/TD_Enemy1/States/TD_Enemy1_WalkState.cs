using UnityEngine;
using System.Collections;
using Prime31.StateKit;

	public class TD_Enemy1_WalkState : SKMecanimState<TD_Enemy1Controller> {

	/// <summary>
	/// Begin this instance.
	/// </summary>
	public override void begin ()
	{
		base.begin ();
		_context.animatorController.PlayState ("WalkRight");
	}

	/// <summary>
	/// Reason this instance.
	/// </summary>
	public override void reason ()
	{
		base.reason ();
	}
	/// <summary>
	/// End this instance.
	/// </summary>
	public override void end ()
	{
		base.end ();
	}

	/// <summary>
	/// Update the specified deltaTime and stateInfo.
	/// </summary>
	/// <param name="deltaTime">Delta time.</param>
	/// <param name="stateInfo">State info.</param>
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		throw new System.NotImplementedException ();
	}

}
