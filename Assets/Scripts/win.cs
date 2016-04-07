using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class win : MonoBehaviour {

    bool go;

	// Use this for initialization
	void Start () {
        go = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (go && !fade.S.fadingOut) {
            SceneManager.LoadScene("Credits");
        }
	}
    void OnTriggerEnter2D(Collider2D coll) {
        go = true;
        fade.S.fadingOut = true;
    }
}
