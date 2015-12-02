using UnityEngine;
using System.Collections;

public class IgnoreCollider : MonoBehaviour {

	public GameObject player;
	//public GameObject 

	// Use this for initialization
	void Start () {
		//player.layer = 8;
		//gameObject.layer = 9;
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTrigger2DEnter (Collider player) {
		//make the parent platform ignore the jumper
		//var platform = transform.parent;
		//Physics.IgnoreCollision(jumper.GetComponent<BoxCollider>(), platform.GetComponent<EdgeCollider2D>());
		Debug.Log ("poop");

		Physics2D.IgnoreLayerCollision (8, 9, true);
		player.GetComponent<BoxCollider2D>().enabled = false;
		player.GetComponent<BoxCollider2D>().enabled = true;
	}
	
	void OnTriggerExit (Collider jumper) {
		//reset jumper's layer to something that the platform collides with
		//just in case we wanted to jump throgh this one
		//jumper.gameObject.layer = 0;
		Physics2D.IgnoreLayerCollision (8, 9, false);
		//re-enable collision between jumper and parent platform, so we can stand on top again
		//var platform = transform.parent;


	}
	
}
