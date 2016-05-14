using UnityEngine;
using System.Collections;

public class ObjectFloat : MonoBehaviour
{
	// Visible in Editor
	public float strength = 1f;

	// Private
	float initialPos;
	AudioSource aud;
	bool collected;
	SpriteRenderer rend;
	
	void Start()
	{
		this.initialPos = this.transform.position.y;
		aud = GetComponent<AudioSource> ();
		rend = GetComponent<SpriteRenderer> ();
		collected = false;
	}
	
	void Update()
	{
		transform.position = new Vector3(
			transform.position.x,
			initialPos + ((float)Mathf.Sin(Time.time) * strength),
			transform.position.z
		);

		if (collected && !aud.isPlaying) {
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D trigger)
	{
		if (!collected && trigger.gameObject.tag == "Player") {
			aud.Play();
			Global.S.collected++;
			collected = true;
			rend.enabled = false;
		}
	}
}
