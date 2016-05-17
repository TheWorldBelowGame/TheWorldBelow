using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public enum FadeState { NONE, FADING_IN, FADING_OUT }

// The Fade class has public static functions for other classes to call in order
// to perform operations like fading in and out. It also has a private reference to
// the actual Fade gameObject on the Canvas, which is set on Start. If we attach this
// Fade script to that object, then the fadeCanvasObj will always point to that object,
// and classes can still call the public static functions for proper functionality.
public class Fade : MonoBehaviour
{
	// Definitions
	public const float defaultFadeTime = 1f;

	// Private Singleton
	public static FadeState fadeState;

	// Private
	static Fade fadeCanvasObj;
	static Coroutine fadeInCoroutine;
	static Coroutine fadeOutCoroutine;
	Image img;

	// Update the static reference to the Fade image on the Canvas
	void Start()
	{
		fadeCanvasObj = this;
		fadeState = FadeState.NONE;
		img = GetComponent<Image>();

		StartCoroutine(FadeInHelper(defaultFadeTime));
	}

	// Fade in from black over a given number of seconds.
	public static void FadeIn(float duration = 1f)
	{
		if (fadeState == FadeState.FADING_IN) {
			Debug.Log("Tried to fade in while already fading in!");
		} else if (fadeState == FadeState.FADING_OUT) {
			fadeCanvasObj.StopCoroutine(fadeOutCoroutine);
		}
		fadeInCoroutine = fadeCanvasObj.StartCoroutine(fadeCanvasObj.FadeInHelper(duration));
	}

	// Fade to black over a given number of seconds.
	// If the scene parameter is set, will change to the give scene at the end of the fade
	public static void FadeOut(float duration = 1f, string scene = null)
	{
		if (fadeState == FadeState.FADING_OUT) {
			Debug.Log("Tried to fade out while already fading out!");
		} else if (fadeState == FadeState.FADING_IN) {
			fadeCanvasObj.StopCoroutine(fadeInCoroutine);
		}
		fadeOutCoroutine = fadeCanvasObj.StartCoroutine(fadeCanvasObj.FadeOutHelper(duration, scene));
	}

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
