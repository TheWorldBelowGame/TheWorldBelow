using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour 
{
	// Definitions
	public delegate void ParallaxCameraDelegate(float deltaMovement);

	// Visible in Editor
	public ParallaxCameraDelegate onCameraTranslate;

	// Private
	float oldPosition;

	void Start()
	{
		oldPosition = transform.position.x;
	}

	void Update()
	{
		if (transform.position.x != oldPosition) {
			if (onCameraTranslate != null) {
				float delta = oldPosition - transform.position.x;
				ParallaxBackground.S.Move(delta);
			}
			oldPosition = transform.position.x;
		}
	}
}