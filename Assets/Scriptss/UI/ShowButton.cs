using UnityEngine;
using System.Collections;

// Attach to a box collider to show a button when the player enters the collider
public class ShowButton : MonoBehaviour
{
	// Visible in Editor
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
