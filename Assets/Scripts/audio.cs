using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class audio : MonoBehaviour {

    public static audio S;
    AudioSource source;
    public AudioClip title_music;
    public AudioClip main_music;

    void OnLevelWasLoaded(int level) {
        print(SceneManager.GetActiveScene().name);
        if (level == SceneManager.GetSceneByName("Menu").buildIndex) {
            source.clip = title_music;
            source.Play();
        } else if (level == SceneManager.GetSceneByName("Main").buildIndex) {
            source.clip = main_music;
            source.Play();
        } else if (level == SceneManager.GetSceneByName("Falling").buildIndex) {
            source.Stop();
        }

    }

    // Use this for initialization
    void Start () {
        S = this;
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name == "Menu") {
            source.clip = title_music;
            source.Play();
        } else if (SceneManager.GetActiveScene().name == "Main") {
            source.clip = main_music;
            source.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
