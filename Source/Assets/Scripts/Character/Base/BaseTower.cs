using System;
using UnityEngine;


public abstract class BaseTower
{
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseTower"/> class.
	/// </summary>
	public BaseTower() {
		
	}

	public virtual void Shoot(){
		Debug.Log ("abc");
	}

}
