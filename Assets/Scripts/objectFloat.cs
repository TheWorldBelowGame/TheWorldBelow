using UnityEngine;
using System.Collections;

public class objectFloat : MonoBehaviour {

	float initialPos;
	public float strength = 1;
	AudioSource aud;
	bool collected;
	SpriteRenderer rend;

	// Use this for initialization
	void Start () {
		this.initialPos = this.transform.position.y;
		aud = GetComponent<AudioSource> ();
		rend = GetComponent<SpriteRenderer> ();
		collected = false;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,
			initialPos + ((float)Mathf.Sin(Time.time) * strength),
			transform.position.z);
		if (collected && !aud.isPlaying)
			gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D trigger) {
		if (!collected && trigger.gameObject.tag == "Player") {
			aud.Play ();
			Global.S.collected++;
			collected = true;
			rend.enabled = false;
		}
	}

}
