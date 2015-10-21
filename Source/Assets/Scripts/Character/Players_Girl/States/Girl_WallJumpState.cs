using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_WallJumpState : SKMecanimState<GirlController>
{
	float timeOnState;
	float girlWalljumpForce = -90.0f;
	public override void begin ()
	{
		base.begin ();
		timeOnState = 0;
		//Pode controlar o jogador no ar
		_context.gameplayController.enableAirControl = false;

		//Recebe os comandos referentes ao eixo horizontal( A & D , <- & ->)
		float horizontal = Input.GetAxis("Horizontal");

		// Velocidade Atual
		Vector2 currentVelocity = _context.physicsController.GetVelocity();

		// Primeiro parametro eh o sentido da forca( Vector2), segundo parametro eh a forca
		// Sentido da forca:
		// em X: nao se altera
		// em Y: O modulo do valor de deslocamento horizontal multiplicado por -1, e isso multiplicado pela forca do pulo
		// Forca propriamente dita: 75
		_context.physicsController.AddForce(new Vector2(currentVelocity.x,
		                                                (Mathf.Abs(horizontal)* -1)), girlWalljumpForce);


		_context.animatorController.PlayState("WallJump");

	}
	public override void reason ()
	{
		base.reason ();
		if(timeOnState > .2f)
		_machine.changeState<Girl_OnAirState>();
		
	}
	
	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{
		timeOnState += deltaTime;
	}
	#endregion

	public override void end ()
	{
		base.end ();
	}

	public void Rotate(float pDirection){
		Vector3 currentScale = _context.transform.localScale;
		
		if(pDirection < 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * -1;
		}else if(pDirection > 0.0f){
			currentScale.x = Mathf.Abs(currentScale.x) * 1;
		}
		_context.transform.localScale = currentScale;
	}

}