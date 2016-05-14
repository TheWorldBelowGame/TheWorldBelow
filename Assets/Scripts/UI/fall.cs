using UnityEngine;
using System.Collections;

public class Fall : MonoBehaviour
{
    public BackgroundScroll background1;
    public BackgroundScroll background2;

    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.CompareTag("Player")) {
			Player.S.Fall();
            background1.Begin();
            background2.Begin();
        }
    }
}
