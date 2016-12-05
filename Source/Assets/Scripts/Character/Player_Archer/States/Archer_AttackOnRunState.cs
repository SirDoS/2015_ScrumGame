using UnityEngine;
using System.Collections;
using Prime31.StateKit;

public class Archer_AttackOnRunState : SKMecanimState<ArcherController> {

	float timeOnState;

	public override void begin ()
	{
		bool landed = false;
		base.begin ();
		timeOnState = 0;

		if(!landed){
			landed = true;
			_context.physicsController.SetVelocity(new Vector2(0.05f, 0.0f));

		}

		if(landed){
			_context.physicsController.SetVelocity(Vector2.zero);
		}

		_context.animatorController.PlayState("Archer_Draw");
	}

	public override void reason ()
	{
		base.reason ();

		if(Input.GetKeyUp(KeyCode.F)){
			GameObject flecha = (GameObject)GameObject.Instantiate(GameObject.Find("Arrow"), 
				new Vector3(_context.physicsController.transform.position.x + 0.2f * _context.physicsController.transform.localScale.x
					, _context.physicsController.transform.position.y + 0.150f, 0),
				_context.physicsController.transform.rotation);

			_context.animatorController.PlayState("Archer_Release");


			flecha.GetComponent<Rigidbody>().AddForce((flecha.transform.right * 500) * _context.physicsController.transform.localScale.x);

		}
		if(timeOnState > 0.333f){
			_machine.changeState<Archer_RunState>();
		}

	}

	public override void update (float deltaTime, AnimatorStateInfo stateInfo)
	{	
		timeOnState += deltaTime;
		
	}

	public override void end ()
	{
		base.end ();
	}
}