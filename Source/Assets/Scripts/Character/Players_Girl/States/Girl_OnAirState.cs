using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Girl_OnAirState : SKMecanimState<GirlController>
{
	public override void begin ()
	{

		base.begin ();

		_machine.animator.Play("OnAir");
	}
	
	public override void reason ()
	{
		float horizontal = Input.GetAxis("Horizontal");

		if(_context.physicsController.IsGrounded()){
			_context.gameplayController.enableAirControl = true;
			if(_context.physicsController.GetVelocity().y < 0.5f){
				if(horizontal == 0){
					_machine.changeState<Girl_IdleState>();
					return;
				}else if(horizontal != 0){
					_machine.changeState<Girl_RunState>();
					return;
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.F)){
			_machine.changeState<Girl_AttackOnAirState>();
			return;
		}
		if(_context.gameplayController.enableAirControl){

			if (horizontal != 0.0f){
				Vector2 currentVelocity = _context.physicsController.GetVelocity();
				_context.physicsController.SetVelocity(new Vector2(horizontal * _context.horizontalMovementSpeed,
				                                                   currentVelocity.y));
		
				Rotate(horizontal);

				RaycastHit2D wallHit = Physics2D.Raycast(_context.Position, 
				                                         new Vector2(horizontal, 0), 0.3f, 
				                                         1 << 10);
				if(wallHit.transform != null && Input.GetKeyDown(KeyCode.Space)){
					Rotate(-horizontal);
					_machine.changeState<Girl_WallJumpState>();
				}
				/*if(wallHit.transform != null)
					Debug.Log(wallHit.transform.name);
				*/
			
			}
		}
		base.reason ();
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
