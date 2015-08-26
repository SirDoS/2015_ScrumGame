using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_AttackOnAirState : SKMecanimState<GirlController>
{
	// Tempo em que esta no estado
	float timeOnState;
	
	public override void begin() {
		base.begin();
		// Tempo eh setado para 0
		timeOnState = 0;
		// Toca a animacao de Ataque no Ar
		_machine.animator.Play("AttackOnAir");
	}
	
	public override void reason (){
		
		base.reason();
		//Se o tempo passado no estado for maior que .2f, vai para o OnAir
		if(timeOnState > 0.2f)
			_machine.changeState<Girl_OnAirState>();
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

		//Recebe a escala atual do jogador
		Vector3 currentScale = _context.transform.localScale;

		//Verifica se ele esta andando para tras
		if(horizontal < 0.0f){
			//Se estiver, multiplica o modulo da escala por -1, para rotaciona-lo no eixo X
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		//Se nao, se estiver indo pra frente
		}else if (horizontal > 0.0f){
			//Define a escala como o modulo dela, tornando assim qualquer numero positivo.
			currentScale.x = Mathf.Abs(currentScale.x);
		}
		//Seta a escala de acordo com as mudancas feitas
		_context.physicsController.SetScale(currentScale);
	}
	#endregion
	
	public override void end (){
		
		base.end();
	}
}
