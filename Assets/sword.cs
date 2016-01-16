using UnityEngine;
using System.Collections;

public class sword : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Enemy") {
            Global.S.killed++;
            Destroy(coll.gameObject);
        }
    }
}
