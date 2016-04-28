using UnityEngine;
using System.Collections;

public class showButton : MonoBehaviour {

    public GameObject button;

	// Use this for initialization
	void Start () {
		if (button != null)
			button.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (button != null)
                button.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (button != null)
                button.SetActive(false);
        }
    }
}
