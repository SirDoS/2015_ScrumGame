using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_IdleState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		//Toca a animacao de Idle
		_machine.animator.Play("Idle");
	}

	public override void reason ()
	{
		base.reason ();
		//Se estiver no chao
		if(_context.physicsController.IsGrounded()){

			//Recebe os comandos referentes ao eixo horizontal( A & D , <- & ->)
			float horizontal = Input.GetAxis("Horizontal");

			// Se estiver se mexendo horizontalmente, passa para o estado Run, pois nao esta mais parado
			if(horizontal != 0.0f){
				_machine.changeState<Girl_RunState>();
				return;
			}
			// Se apertar a tecla ESPACO, passa para o estado de Pulo
			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Girl_JumpState>();
				return;
			}
			// Se apertar F, passa para o estado de ataque
			if(Input.GetKeyDown(KeyCode.F)){
				_machine.changeState<Girl_AttackState>();
				return;
			}
			//Define a velocidade como 0, em x e y
			_context.physicsController.SetVelocity(Vector2.zero);
		}
		// Caso nenhuma das situacoes acima se apliquem, eh porque ele esta caindo, portanto vai para o OnAir
		else{
			_machine.changeState<Girl_OnAirState>();
			return;
		}
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{

	}
	#endregion

	public override void end ()
	{
		base.end ();
	}
}
