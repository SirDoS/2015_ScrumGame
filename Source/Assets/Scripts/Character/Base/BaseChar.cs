using UnityEngine;
using System.Collections;

// Classe de base para os personagens do jogo, herda de BaseActor
public class BaseChar : BaseActor
{	
	//Velocidade de movimentacao horizontal e vertical
	public float horizontalMovementSpeed;
	public float verticalMovementSpeed;

	//Instancias dos controladores de gameplay, fisica, e animacao
	public GameplayController gameplayController;
	public PhysicsController physicsController;
	public AnimationController animatorController;
	public AttackController attackController;

	/**
	 *	Metodo que aplica dano. 
	 *	Recebe como parametro a quantidade de dano, e quem eh o causador de dano. 
	 */
	public void doDamage (float pDamage, GameObject pCauser)
	{
		
	}

	/**
	 *	Metodo para receber dano. 
	 */
	public void receiveDamage(){

	}
}