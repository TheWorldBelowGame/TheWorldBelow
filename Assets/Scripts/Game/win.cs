using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public string goodEnding;
    public string badEnding;
    string scene;
	
	void Start()
	{
        scene = badEnding;
	}

    void OnTriggerEnter2D(Collider2D coll)
	{
        fade.S.fadeOut();

        if (Global.S.collected >= 10) {
            scene = goodEnding;
        }
        fade.S.whenDone(scene);
    }
}
