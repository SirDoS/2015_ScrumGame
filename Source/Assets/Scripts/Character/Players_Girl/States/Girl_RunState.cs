using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_RunState : SKMecanimState<GirlController>
{
	public override void begin ()
	{
		base.begin ();

		_machine.animator.Play("Run");

	}
	public override void reason ()
	{
		base.reason ();
		if(_context.physicsController.IsGrounded()){

			float horizontal = Input.GetAxis("Horizontal");

			if(horizontal == 0.0f){
				_machine.changeState<Girl_IdleState>();
				return;
			}

			Vector3 currentScale = _context.transform.localScale;

			if(horizontal < 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x) * -1;
			}else if (horizontal > 0.0f){
				currentScale.x = Mathf.Abs(currentScale.x) * 1;
			}

			_context.transform.localScale = currentScale;

			if(Input.GetKeyDown(KeyCode.F)){
				_machine.changeState<Girl_AttackOnRunState>();
				return;
			}

			if(Input.GetKeyDown(KeyCode.Space)){
				_machine.changeState<Girl_JumpState>();
				return;
			}
		}else{
			_machine.changeState<Girl_OnAirState>();
			return;
		}

	}

	#region implemented abstract members of SKMecanimState
	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{	
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

	public override void end ()
	{
		base.end ();
	}
}
