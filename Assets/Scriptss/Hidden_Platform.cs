using UnityEngine;
using System.Collections;

public class Hidden_Platform : MonoBehaviour {

    private SpriteRenderer sprend;
    public BoxCollider2D coll;
    void Start()
    {
        sprend = gameObject.GetComponent<SpriteRenderer>();
    }

    public void togglePlatform()
    {

        if (sprend.enabled)
        {
            sprend.enabled = false;
            coll.enabled = false;
        }
        else
        {
            sprend.enabled = true;
            coll.enabled = true;
        }
    }
}
