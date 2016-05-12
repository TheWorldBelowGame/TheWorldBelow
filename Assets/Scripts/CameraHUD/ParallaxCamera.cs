using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]

public class ParallaxCamera : MonoBehaviour 
{
	public delegate void ParallaxCameraDelegate(float deltaMovement);
	public ParallaxCameraDelegate onCameraTranslate;
	private float oldPosition;
	void Start()
	{
		oldPosition = transform.position.x;
	}
	void Update()
	{
		if (transform.position.x != oldPosition)
		{
			if (onCameraTranslate != null)
			{
				float delta = oldPosition - transform.position.x;
				ParallaxBackground.S.Move(delta);
			}
			oldPosition = transform.position.x;
		}
	}
}