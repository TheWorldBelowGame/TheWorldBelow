using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour
{
	// Visible in Editor
    public BackgroundScroll background1;
    public BackgroundScroll background2;

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Player")) {
            background1.Begin();
            background2.Begin();
        }
    }
}
