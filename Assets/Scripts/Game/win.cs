using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {
    
    public string good_ending;
    public string bad_ending;
    string scene;

	// Use this for initialization
	void Start () {
        scene = bad_ending;
	}
	
	// Update is called once per frame
	void Update () {

	}
    void OnTriggerEnter2D(Collider2D coll) {
        fade.S.fadeOut();
        if (Global.S.collected >= 10) {
            scene = good_ending;
        }
        fade.S.whenDone(scene);
    }
}
