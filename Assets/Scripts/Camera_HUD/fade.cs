using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fade : MonoBehaviour {

    public static fade S;

    public float duration = 1f;

    Image img;
    public bool fadingIn;
    public bool fadingOut;
    public bool changeScene;
    public string scene = "";
    float t;

	// Use this for initialization
	void Start () {
        S = this;

        img = GetComponent<Image>();
        fadingIn = true;
        fadingOut = false;
        changeScene = false;
        t = 0;
        img.color = Color.black;
    }
	
	// Update is called once per frame
	void Update () {
        if (fadingIn) {
            fadeIn();
        }
        if (fadingOut) {
            fadeOut();
        }
        if (changeScene && !fadingOut) {
            SceneManager.LoadScene(scene);
        }
        if (Input.GetKeyDown(KeyCode.X))
            fadingIn = true;
        if (Input.GetKeyDown(KeyCode.Z))
            fadingOut = true;
    }

    void fadeIn() {
        img.color = Color.Lerp(Color.black, Color.clear, (t) / duration);
        t += Time.deltaTime;
        if (img.color.a < 0.05) {
            img.color = Color.clear;
            fadingIn = false;
            t = 0;
        }
    }

    void fadeOut() {
        img.color = Color.Lerp(Color.black, Color.clear, (duration - t) / duration);
        t += Time.deltaTime;
        if (img.color.a > 0.95) {
            img.color = Color.black;
            fadingOut = false;
            t = 0;
        }
    }
}
