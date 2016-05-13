using UnityEngine;
using System.Collections;

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
