using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class audio : MonoBehaviour {

    public static audio S;
    AudioSource source;
    public AudioClip title_music;
    public AudioClip main_music;
	public AudioClip wind;

	void Awake() {
		S = this;
        Input_Managment.init();
	}

    void OnLevelWasLoaded(int level) {
		if (S != this) {
			Destroy (gameObject);
			return;
		}
		if (level == SceneManager.GetSceneByName ("Menu").buildIndex) {
			if (source.clip != title_music) {
				source.clip = title_music;
				source.Play ();
				Global.S.collected = 0;
			}
		} else if (level == SceneManager.GetSceneByName ("Falling").buildIndex) {
			if (source.clip != wind) {
				source.clip = wind;
				source.Play ();
			}
		} else if (level == SceneManager.GetSceneByName("Main").buildIndex) {
			if (source.clip != main_music) {
				source.clip = main_music;
				source.Play ();
			}
        } else if (level == SceneManager.GetSceneByName("Falling").buildIndex) {
            source.Stop();
        }

    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();

        source.clip = title_music;
        source.Play();

        UnityEngine.Cursor.visible = false;
        Global.S.collected = 0;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
