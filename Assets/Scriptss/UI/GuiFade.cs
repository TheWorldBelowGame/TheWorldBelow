using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GuiFade : MonoBehaviour
{
	// Visible in Editor
	public Color endColor;
	public float duration;
	public float delay = 0;

	// Private
	Color startColor;
	Text txt;
	float t;
	float i;
	bool done;
	bool start;
	
	void Start()
	{
		startColor = new Color(0, 0, 0, 0);
		txt = GetComponent<Text>();
		txt.color = startColor;
		t = 0;
		i = 0;
		done = false;
		start = false;
	}

	void Update()
	{
		i += Time.deltaTime;

		if (i >= delay) {
			start = true;
		}

		if (start && !done) {
			Fade();
		}
	}

	void Fade()
	{
		txt.color = Color.Lerp(startColor, endColor, (t) / duration);
		t += Time.deltaTime;
		if (txt.color.a > 0.95) {
			txt.color = endColor;
			done = true;
		}
	}

}
