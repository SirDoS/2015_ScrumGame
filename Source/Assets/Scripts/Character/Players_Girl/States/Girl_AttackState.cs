using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackState : SKMecanimState<GirlController>
{
	//Tempo decorrido no estado
	float timeOnState;

	public override void begin () {
		base.begin();
		//Seta o tempo para 0
		timeOnState = 0;
		Debug.Log("Im attacking, bitch!");
		//Toca a animacao de Ataque
		_machine.animator.Play("Attack");
		_context.attackController.Attack();
	}
	
	public override void reason (){
		base.reason();
		//Se o tempo for maior que .5f
		if(timeOnState > 0.5f){

			//Recebe os comandos referentes ao eixo horizontal( A & D , <- & ->)
			float horizontal = Input.GetAxis("Horizontal");

			// Se movimento horizontal for 0, ele esta parado, portanto vai para o Idle
			if(horizontal == 0){
				_machine.changeState<Girl_IdleState>();
				return;
			// Se nao, se o movimento horizontal for diferente de 0, ele esta correndo, portanto vai para o Run
			}else if(horizontal != 0){
				_machine.changeState<Girl_RunState>();
				return;
			}
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		// Incrementa o tempo passado no estado, de acordo com deltaTiem
		timeOnState += deltaTime;
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
