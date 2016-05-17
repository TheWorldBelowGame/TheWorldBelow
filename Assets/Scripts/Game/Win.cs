using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
	// Visible in Editor
    public string goodEnding;
    public string badEnding;

	// Private
    string scene;
	
	void Start()
	{
        scene = badEnding;
	}

    void OnTriggerEnter2D(Collider2D coll)
	{
        if (Global.S.collected >= 10) {
            scene = goodEnding;
        }
		Fade.FadeOut(1f, scene);
	}
}
