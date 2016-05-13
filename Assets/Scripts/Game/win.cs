using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    bool go;
    public string goodEnding;
    public string badEnding;
    string scene;
	
	void Start()
	{
        go = false;
        scene = badEnding;
	}
	
	void Update()
	{
        if (go && !fade.S.fadingOut) {
            SceneManager.LoadScene(scene);
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
	{
        go = true;
        fade.S.fadingOut = true;
        if (Global.S.collected >= 10) {
            scene = goodEnding;
        }
    }
}
