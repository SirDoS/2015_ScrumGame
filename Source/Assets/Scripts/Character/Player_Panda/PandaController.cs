using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class PandaController : BaseChar {

	public float pandaJumpForce = 500.0f;

	public SKMecanimStateMachine<PandaController> pandaStateMachine;

	// Use this for initialization
	void Start () {
		currentLife = maxLife;
		isAlive = true;
		pandaStateMachine = new SKMecanimStateMachine<PandaController>(animatorController.Animator, this, new Panda_IdleState());
		pandaStateMachine.addState(new Panda_IdleState());
		pandaStateMachine.addState(new Panda_RunState());
		pandaStateMachine.addState(new Panda_JumpState());
		pandaStateMachine.addState(new Panda_OnAirState());
		pandaStateMachine.addState(new Panda_WallJumpState());
		pandaStateMachine.addState(new Panda_AttackOnIdleState());
		pandaStateMachine.addState(new Panda_AttackOnRunState());
		pandaStateMachine.addState(new Panda_AttackOnAirState());
		pandaStateMachine.addState(new Panda_OnDeathState());

		pandaStateMachine.onStateChanged += () => {
			Debug.Log(pandaStateMachine.currentState.ToString());
		};
	}
	
	// Update is called once per frame
	void Update () {
		pandaStateMachine.update(Time.deltaTime);
		if(!isAlive)
			pandaStateMachine.changeState<Panda_OnDeathState>();
	}

	void OnGUI()
	{
		if(pandaStateMachine != null)
		{
			GUILayout.Box(pandaStateMachine.currentState.ToString());
		}
	}
}
