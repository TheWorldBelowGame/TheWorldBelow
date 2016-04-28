using UnityEngine;
using System.Collections;

public class Hidden_Platform : MonoBehaviour {

    private SpriteRenderer sprend;
    void Start()
    {
        sprend = gameObject.GetComponent<SpriteRenderer>();
    }

    public void togglePlatform()
    {
        if (sprend.enabled) sprend.enabled = false;
        else sprend.enabled = true;
    }
}
