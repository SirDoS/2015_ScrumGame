using UnityEngine;
using System.Collections;

public class BaseActor : MonoBehaviour
{
	// Armazena uma instancia de Transform, pois o custo de sempre chamar essa classe eh alto.
	private Transform cachedTransform;

	// Por padrao o jogador esta vivo
	[SerializeField] 
	protected bool isAlive = true;
	//Sua vida maxima eh 100
	protected int maxLife = 100;

	public int SYNCADEMONIO = 666;

	//Vida atual eh 100
	public int currentLife = 100;

	// Getter do cachedTransform, para retornar sua posicao 
	public Vector3 Position{
		get {// Se cachedTransform for nulo, ele recebe transform(uma instancia de Transform), deixando assim de ser nulo
			if(cachedTransform == null){
				cachedTransform = transform;
			}
			// retorna a posicao de cachedTransform
			return cachedTransform.position;
		}
				
		
	}

	// Metodo para aplicacao de Dano
	public virtual void Damage(int pDamage)
	{

	}

}