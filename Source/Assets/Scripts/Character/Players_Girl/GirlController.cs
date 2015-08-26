using UnityEngine;
using System.Collections;
using Prime31.StateKit;
// Classe da menina que herda de BaseChar
public class GirlController : BaseChar
{
	// Forca do pulo da menina
	public float girlJumpForce = 500.0f;

	// Declaracao da maquina de estados
	public SKMecanimStateMachine<GirlController> stateMachine;

	void Start()
	{
		// Vida atual recebe o maximo de vida possivel
		currentLife = maxLife;
		// Esta vivo
		isAlive = true;

		stateMachine = new SKMecanimStateMachine<GirlController>(animatorController.Animator,
		                                                         this, new Girl_IdleState());

		//Todos os estados da menina sao adicionados na maquina de estado
		stateMachine.addState(new Girl_IdleState());
		stateMachine.addState(new Girl_RunState());
		stateMachine.addState(new Girl_JumpState());
		stateMachine.addState(new Girl_OnAirState());
		stateMachine.addState(new Girl_AttackOnAirState());
		stateMachine.addState(new Girl_AttackState());
		stateMachine.addState(new Girl_AttackOnRunState());
		stateMachine.addState(new Girl_WallJumpState());

		// Quando passa de um estado para outro, imprime na tela este estado
		stateMachine.onStateChanged += () => {
			Debug.Log(stateMachine.currentState.ToString());
		};

	}
	
	void Update()
	{
		stateMachine.update(Time.deltaTime);

	}
	// Debugger para mostrar o estado atual na tela
	void OnGUI()
	{
		if(stateMachine != null)
		{
			GUILayout.Box(stateMachine.currentState.ToString());
		}
	}
}