using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

    public GameObject inside;
    public GameObject outside;

    public bool outside_on;

	// Use this for initialization
	void Start () {
        outside_on = true;
        outside.gameObject.SetActive(true);
        inside.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void in_out() {
        if (outside_on) {
            outside.gameObject.SetActive(false);
            inside.gameObject.SetActive(true);
        } else {
            outside.gameObject.SetActive(true);
            inside.gameObject.SetActive(false);
        }
        outside_on = !outside_on;
    }
}
