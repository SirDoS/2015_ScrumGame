using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_OnAirState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();
		//Toca a animacao de estar no ar
		_machine.animator.Play("OnAir");
	}
	
	public override void reason ()
	{
		//Recebe os comandos referentes ao eixo horizontal( A & D , <- & ->)
		float horizontal = Input.GetAxis("Horizontal");

		//Se ele estiver no chao, sera possivel controlar o jogador no ar
		if(_context.physicsController.IsGrounded()){
			_context.gameplayController.enableAirControl = true;

			//Se sua velocidade em Y for menor que 0.5f
				//Se nao houver movimento horizontal, ele esta parado, portanto vai pra Idle
			if(_context.physicsController.GetVelocity().y < 0.5f){
				if(horizontal == 0){
					_machine.changeState<Girl_IdleState>();
					return;
				// Se nao, se houver movimento horizontal, passa para o Run
				}else if(horizontal != 0){
					_machine.changeState<Girl_RunState>();
					return;
				}
			}
		}
		//Se apertou F, esta Atacando, portanto vai para o estado de Ataque no Ar
		if(Input.GetKeyDown(KeyCode.F)){
			_machine.changeState<Girl_AttackOnAirState>();
			return;
		}
		//Se ele puder controlar no ar
		if(_context.gameplayController.enableAirControl){
			//Se estiver se movimentando na horizontal
			if (horizontal != 0.0f){
				//Recebe a velocidade atual
				Vector2 currentVelocity = _context.physicsController.GetVelocity();
				//Em X: define de acordo com o apertar dos botoes referentes a horizontal vezes a velocidadeDeMovimentoHorizontal
				//Em Y: recebe a velocidade em que se encontra
				_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
				                                                   currentVelocity.y));

				//Rotaciona o jogado de acordo com o lado para qual ele esta andando
				Rotate(horizontal);

				//Raycast para verificar se ha uma parede na frente da menina
				// Parametros sao, respectivamente, origem do raio, direcao, distancia, e qual layer ira verificar(Wall)
				RaycastHit2D wallHit = Physics2D.Raycast(_context.Position, 
				                                         new Vector2(horizontal, 0), 0.3f, 
				                                         1 << 10);

				//Se o wallhit nao for nulo, e o for apertado o ESPACO
				if(wallHit.transform != null && Input.GetKeyDown(KeyCode.Space)){
					//Rotaciona o jogador , de acordo com o contrario da movimentacao dele horizontalmente
					Rotate(-horizontal);
					//Passa para o estado de pulo na parede
					_machine.changeState<Girl_WallJumpState>();
				}

				/* Debugger para mostrar o nome do objeto em quem o raio toca
				* if(wallHit.transform != null)
				*Debug.Log(wallHit.transform.name);
				*/
			
			}
		}
		base.reason ();
	}

	//Metodo para rotacionar o jogador de acordo com o lado para que ele esta indo
	public void Rotate(float pDirection){
		Vector3 currentScale = _context.transform.localScale;
		
		if(pDirection < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if(pDirection > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * 1;
		}
		_context.transform.localScale = currentScale;
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
