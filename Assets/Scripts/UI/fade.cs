using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
	// Definitions
	public const float defaultFadeTime = 1f;

	// Private Singleton
	static Fade S;

	// Private
	Image img;

	void Start()
	{
		S = this;
		img = GetComponent<Image>();

		StartCoroutine(FadeInHelper(defaultFadeTime));
	}

	public static void FadeIn(float duration = 1f)
	{
		S.StartCoroutine(S.FadeInHelper(duration));
	}

	public static void FadeOut(float duration = 1f, string scene = null)
	{
		S.StartCoroutine(S.FadeOutHelper(duration, scene));
	}

	// Fade in from black over a given number of seconds.
	IEnumerator FadeInHelper(float duration)
	{
		img.color = Color.black;
		float timer = 0;

		while (img.color.a > 0.05) {
			timer += Time.deltaTime;
			img.color = Color.Lerp(Color.black, Color.clear, timer / duration);
			yield return new WaitForEndOfFrame();
		}

		img.color = Color.clear;
	}

	// Fade to black over a given number of seconds.
	// If the scene parameter is set, will change to the give scene at the end of the fade
	IEnumerator FadeOutHelper(float duration, string scene)
	{
		img.color = Color.clear;
		float timer = 0;

		while (img.color.a < 0.95) {
			timer += Time.deltaTime;
			float lastFrame = (duration - timer) / duration;
			img.color = Color.Lerp(Color.black, Color.clear, lastFrame);
			yield return new WaitForEndOfFrame();
		}

		img.color = Color.black;

		if (scene != null) {
			SceneManager.LoadScene(scene);
		}
	}
}
