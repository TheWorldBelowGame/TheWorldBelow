using UnityEngine;
using System.Collections;

public class objectFloat : MonoBehaviour {

	float initialPos;
	public float strength = 1;

	// Use this for initialization
	void Start () {
		this.initialPos = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x,
			initialPos + ((float)Mathf.Sin(Time.time) * strength),
			transform.position.z);
	}

}
