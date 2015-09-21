using UnityEngine;
using System.Collections;

public class SkyColorScript : MonoBehaviour {

	public Color [] skyColorTrasitions;

	public Color currentColor;

	public SpriteRenderer skyRenderer;

	private int nextColorIndex = 0;

	public float transitionSpeed = 1.0f;


	void Update()
	{

		currentColor = Color.Lerp(currentColor, skyColorTrasitions[nextColorIndex], Time.deltaTime * transitionSpeed);

		if(Mathf.Abs((currentColor.r - skyColorTrasitions[nextColorIndex].r)) <= 0.01f 
		   && Mathf.Abs((currentColor.g - skyColorTrasitions[nextColorIndex].g)) <= 0.01f
		   && Mathf.Abs((currentColor.b - skyColorTrasitions[nextColorIndex].b)) <= 0.01f)
		{
			Debug.Log("Oi "+nextColorIndex);
			nextColorIndex++;
			if(nextColorIndex >= skyColorTrasitions.Length)
			{
				nextColorIndex = 0;
			}
		}

		skyRenderer.color = currentColor;

	}
}
