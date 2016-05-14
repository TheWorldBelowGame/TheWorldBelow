using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade S;
    public float duration = 1f;

    bool _fadingIn;
    bool _fadingOut;
	public bool fadingIn { get { return _fadingIn; } }
	public bool fadingOut { get { return _fadingOut; } }

	Image img;
	bool changeScene;
    string scene = "";
    float t;
	
	void Start()
	{
        S = this;

        img = GetComponent<Image>();
        _fadingIn = true;
        _fadingOut = false;
        changeScene = false;
        t = 0;
        img.color = Color.black;
    }
	
	void Update()
	{
        if (fadingIn) {
            StepIn();
        }
        if (fadingOut) {
            StepOut();
        }
        if (changeScene && !fadingOut) {
            SceneManager.LoadScene(scene);
        }
    }

    public bool FadeIn(float d = 1)
	{
        if (fadingIn || fadingOut) {
            return false;
        }
        duration = d;
        _fadingIn = true;
        return true;
    }

    public bool FadeOut(float d = 1)
	{
        if (fadingIn || fadingOut) {
            return false;
        }
        duration = d;
        _fadingOut = true;
        return true;
    }

    public void WhenDone(string s)
	{
        changeScene = true;
        scene = s;
    }

    void StepIn()
	{
        img.color = Color.Lerp(Color.black, Color.clear, (t) / duration);
        t += Time.deltaTime;
        if (img.color.a < 0.05) {
            img.color = Color.clear;
            _fadingIn = false;
            t = 0;
        }
    }

    void StepOut()
	{
        img.color = Color.Lerp(Color.black, Color.clear, (duration - t) / duration);
        t += Time.deltaTime;
        if (img.color.a > 0.95) {
            img.color = Color.black;
            _fadingOut = false;
            t = 0;
        }
    }
}
