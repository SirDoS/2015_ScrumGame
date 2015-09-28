using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_JumpState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		// Velocidade atual recebe a velocidade passada pelo controlador de Fisica
		Vector2 currentVelocity = _context.physicsController.GetVelocity();

		// Define a velocidade
		// em X: como sua velocidade atual
		// em Y: de acordo com a forca de pulo da menina
		//_context.physicsController.SetVelocity(new Vector2(currentVelocity.x,
		                                                   //_context.girlJumpForce));
		_context.physicsController.AddForce(new Vector2(currentVelocity.x, _context.girlJumpForce), 2);

		//Toca a animacao de Pulo
		_machine.animator.Play("Jump");
	}

	public override void reason(){
		base.reason ();
		//Passa para o estado OnAir, pois a forca do pulo ja foi aplicada
		_machine.changeState<Girl_OnAirState>();
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
