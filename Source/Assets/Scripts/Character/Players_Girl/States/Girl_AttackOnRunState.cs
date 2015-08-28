using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackOnRunState : SKMecanimState<GirlController>
{
	// Tempo decorrido no estado
	float timeOnState;
	
	public override void begin () {
		base.begin();
		//Seta o tempo para 0
		timeOnState = 0;
		//Toca a animacao de Ataque
		_machine.animator.Play("Attack");
		_context.attackController.Attack();
	}
	
	public override void reason (){
		base.reason();



		if(timeOnState > 0.5f){

			// horizontal recebe os inputs referentes ao eixo horizontal((A & D) ou (<- & ->))
			float horizontal = Input.GetAxis("Horizontal");

			// Se nao estiver se mexendo na horizontal, esta parado, portanto passa para Idle
			if(horizontal == 0){
				_machine.changeState<Girl_IdleState>();
				return;
			// Caso esteja se mexendo, vai para o estado Run
			}else if(horizontal != 0){
				_machine.changeState<Girl_RunState>();
				return;
			}
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		//Incrementa o tempo, usando o tempo atual + deltaTime
		timeOnState += deltaTime;
		
		// horizontal recebe os inputs referentes ao eixo horizontal((A & D) ou (<- & ->))
		float horizontal = Input.GetAxis("Horizontal");
		
		// Velocidade atual recebe a velocidade passada pelo controlador de Fisica
		Vector2 currentVelocity = _context.physicsController.GetVelocity();
		// Define a velocidade de acordo com o apertar dos botoes 
		// No eixo X : ((A & D) ou (<- & ->)) e a velocidade na horizontal
		// No eixo Y : recebe a velocidade em que ele esta
		_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed
		                                                   , currentVelocity.y));
	}
	#endregion
	
	public override void end (){
		base.end();
	}
}
