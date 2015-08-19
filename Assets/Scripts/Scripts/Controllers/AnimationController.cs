using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
	private Animator animator;


	public Animator Animator {
		get {
			return animator;
		}
	}

	void Awake()
	{
		if(animator == null)
			animator = GetComponent<Animator>();
	}
}
