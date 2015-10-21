using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
	private Animator animator;


	public Animator CachedAnimator {
		get {
			if(animator == null)
				animator = GetComponent<Animator>();
			   
			return animator;
		}
	}

	public void PlayState (string pStateName){
		CachedAnimator.Play(pStateName);
	}
}
