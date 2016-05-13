using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
	public static ParallaxBackground S;
	public ParallaxCamera parallaxCamera;

	List<ParallaxLayer> parallaxLayers;

	void Awake()
	{
		S = this;
		parallaxLayers = new List<ParallaxLayer>();
	}

	void Start()
	{
		if (parallaxCamera == null) {
			parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
		}

		if (parallaxCamera != null) {
			parallaxCamera.onCameraTranslate += Move;
		}

		SetLayers();
	}

	void SetLayers()
	{
		parallaxLayers.Clear();
		for (int i = 0; i < transform.childCount; i++) {
			ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();
			if (layer != null) {
				parallaxLayers.Add(layer);
			}
		}
	}

	public void Move(float delta)
	{
		foreach (ParallaxLayer layer in parallaxLayers) {
			layer.Move(delta);
		}
	}
}