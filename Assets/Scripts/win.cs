using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {

    bool go;
    public string good_ending;
    public string bad_ending;
    string scene;

	// Use this for initialization
	void Start () {
        go = false;
        scene = bad_ending;
	}
	
	// Update is called once per frame
	void Update () {
        if (go && !fade.S.fadingOut) {
            SceneManager.LoadScene(scene);
        }
	}
    void OnTriggerEnter2D(Collider2D coll) {
        go = true;
        fade.S.fadingOut = true;
        if (Global.S.collected >= 10) {
            scene = good_ending;
        }
    }
}
