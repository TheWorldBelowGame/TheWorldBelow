using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public static Audio S;

    public AudioClip titleMusic;
    public AudioClip mainMusic;
	public AudioClip wind;

	AudioSource source;

	void Awake()
	{
		S = this;
	}

    void OnLevelWasLoaded(int level)
	{
		if (S != this) {
			Destroy(gameObject);
			return;
		}
		if (level == SceneManager.GetSceneByName("Menu").buildIndex) {
			if (source.clip != titleMusic) {
				source.clip = titleMusic;
				source.Play();
				Global.S.collected = 0;
			}
		} else if (level == SceneManager.GetSceneByName("Falling").buildIndex) {
			if (source.clip != wind) {
				source.clip = wind;
				source.Play();
			}
		} else if (level == SceneManager.GetSceneByName("Main").buildIndex) {
			if (source.clip != mainMusic) {
				source.clip = mainMusic;
				source.Play();
			}
        } else if (level == SceneManager.GetSceneByName("Falling").buildIndex) {
            source.Stop();
        }

    }
	
    void Start()
	{
        DontDestroyOnLoad(gameObject);

        source = GetComponent<AudioSource>();

        source.clip = titleMusic;
        source.Play();

		// TODO: move these lines elsewhere
        UnityEngine.Cursor.visible = false;
        Global.S.collected = 0;
    }
}
