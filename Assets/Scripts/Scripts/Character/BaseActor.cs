using UnityEngine;
using System.Collections;

public class BaseActor : MonoBehaviour
{
	[SerializeField]
	protected bool isAlive = true;
	protected int maxLife = 100;
	
	public int currentLife = 100;

	public virtual void Damage(int pDamage)
	{

	}
}