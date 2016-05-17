using UnityEngine;
using System.Collections;

// Behavior for the player's attack. Controlled by enabling and disabling this script.
public class Sword : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.gameObject.tag == "Enemy") {
            Global.S.killed++;
            coll.GetComponent<Enemy>().Die();
        }
    }
}
