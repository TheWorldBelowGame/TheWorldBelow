using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour
{
	// Visible in Editor
	public float parallaxFactor;

	public void Move(float delta)
	{
		Vector3 newPos = transform.localPosition;
		newPos.x -= delta * parallaxFactor;
		transform.localPosition = newPos;
	}
}