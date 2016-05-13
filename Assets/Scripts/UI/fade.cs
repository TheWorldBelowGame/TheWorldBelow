using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour {

    public static fade S;

    public float duration = 1f;

    Image img;

    private bool _fadingIn;
    private bool _fadingOut;

    public bool fadingIn { get { return _fadingIn; } }
    public bool fadingOut { get { return _fadingOut; } }

    bool changeScene;
    string scene = "";
    float t;

	// Use this for initialization
	void Start () {
        S = this;

        img = GetComponent<Image>();
        _fadingIn = true;
        _fadingOut = false;
        changeScene = false;
        t = 0;
        img.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingIn) {
            stepIn();
        }
        if (fadingOut) {
            stepOut();
        }
        if (changeScene && !fadingOut) {
            SceneManager.LoadScene(scene);
        }
        /*if (Input.GetKeyDown(KeyCode.X))
            fadingIn = true;
        if (Input.GetKeyDown(KeyCode.Z))
            fadingOut = true;*/
    }

    public bool fadeIn(float d = 1) {
        if (fadingIn || fadingOut) {
            return false;
        }
        duration = d;
        _fadingIn = true;
        return true;
    }

    public bool fadeOut(float d = 1) {
        if (fadingIn || fadingOut) {
            return false;
        }
        duration = d;
        _fadingOut = true;
        return true;
    }

    public void whenDone(string s) {
        changeScene = true;
        scene = s;
    }

    void stepIn() {
        img.color = Color.Lerp(Color.black, Color.clear, (t) / duration);
        t += Time.deltaTime;
        if (img.color.a < 0.05) {
            img.color = Color.clear;
            _fadingIn = false;
            t = 0;
        }
    }

    void stepOut() {
        img.color = Color.Lerp(Color.black, Color.clear, (duration - t) / duration);
        t += Time.deltaTime;
        if (img.color.a > 0.95) {
            img.color = Color.black;
            _fadingOut = false;
            t = 0;
        }
    }
}
