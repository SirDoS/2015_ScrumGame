using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Classe que tem atributos para servir de base a tudo que tem vida, nao necessariamente um personagem
public class BaseActor : MonoBehaviour
{

	public delegate void OnDeathDelegate(BaseActor pVictim);

	public List<BaseActor> whoHit;

	public OnDeathDelegate onDeathCallback;

	// Armazena uma instancia de Transform, pois o custo de sempre chamar essa classe eh alto.
	private Transform cachedTransform;

	// Por padrao o jogador esta vivo
	[SerializeField] 
	public bool isAlive = true;
	//Sua vida maxima eh 100
	protected int maxLife = 100;

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
	public virtual void DoDamage(int pDamage, BaseActor pTarget)
	{
		if(pTarget.isAlive)
			pTarget.ReceiveDamage(pDamage, this);

	}

	// Metodo para recebicao de Dano
	public virtual void ReceiveDamage(int pDamage, BaseActor pBully)
	{
		if(!whoHit.Contains(pBully)){
			onDeathCallback += pBully.OnMurder;
			whoHit.Add(pBully);
		}

		currentLife -= pDamage;

		if(currentLife <= 0)
			OnDeath();
	}

	public virtual void OnDeath(int pDamage = 0, BaseActor pKiller = null){
		whoHit.Clear();

		isAlive = false;

		if(onDeathCallback != null)
			onDeathCallback(this);

		onDeathCallback = null;
	}

	public virtual void OnMurder(BaseActor pVictim){
		Debug.Log (this.name + " matou " +  pVictim.name);
	}

	public virtual void Reset(){
		currentLife = maxLife;
		isAlive = true;
	}

}