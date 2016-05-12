using UnityEngine;
using System.Collections;

public class ShowButton : MonoBehaviour
{
    public GameObject button;
	
	void Start ()
	{
		if (button != null) {
			button.SetActive(false);
		}
	}

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Player") && button != null) {
			button.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
	{
        if (other.CompareTag("Player") && button != null) {
			button.SetActive(false);
        }
    }
}
