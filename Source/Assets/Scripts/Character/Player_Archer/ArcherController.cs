using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class ArcherController : BaseChar {
	
	public SKMecanimStateMachine<ArcherController> archerStateMachine;

	// Use this for initialization
	void Start () {
		currentLife = maxLife;
		isAlive = true;
		archerStateMachine = new SKMecanimStateMachine<ArcherController>(animatorController.CachedAnimator, this, new Archer_IdleState());
		archerStateMachine.addState(new Archer_IdleState());
		archerStateMachine.addState(new Archer_RunState());
		archerStateMachine.addState(new Archer_JumpState());
		archerStateMachine.addState(new Archer_OnAirState());
		archerStateMachine.addState(new Archer_WallJumpState());
		archerStateMachine.addState(new Archer_AttackOnIdleState());
		archerStateMachine.addState(new Archer_AttackOnRunState());
		archerStateMachine.addState(new Archer_AttackOnAirState());
		archerStateMachine.addState(new Archer_OnDeathState());

		/*
		archerStateMachine.onStateChanged += () => {
			Debug.Log(archerStateMachine.currentState.ToString());
		};
		*/
	}

	// Update is called once per frame
	void Update () {
		archerStateMachine.update(Time.deltaTime);
		if(!isAlive)
			archerStateMachine.changeState<Archer_OnDeathState>();
	}

	void OnGUI()
	{
		if(archerStateMachine != null)
		{
			GUILayout.Box(archerStateMachine.currentState.ToString());
		}
	}
}
