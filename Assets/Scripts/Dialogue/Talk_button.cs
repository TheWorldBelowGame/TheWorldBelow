using UnityEngine;
using System.Collections;

public class Talk_button : MonoBehaviour {

	public Sprite button;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		Color tmp = GetComponent<SpriteRenderer> ().color;


		if (coll.gameObject.tag == "Player") {
			Debug.Log ("inside");
			tmp.a = 1f;
			GetComponent<SpriteRenderer> ().color = tmp;
		} else {
			tmp.a = 0f;
			GetComponent<SpriteRenderer> ().color = tmp;
		}
	}
}
