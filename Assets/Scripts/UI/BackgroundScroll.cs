using UnityEngine;
using System.Collections;

public class BackgroundScroll : MonoBehaviour
{
	// Visible in Editor
    public float scrollSpeed;
    public float tileSizeZ;

	// Private
	Vector3 startPosition;
	bool isScrolling;
    float startTime;

    void Start()
	{
        isScrolling = false;
        startPosition = transform.localPosition;
    }

    void Update()
	{
        if (isScrolling) {
            float newPosition = Mathf.Repeat((Time.time - startTime) * scrollSpeed, tileSizeZ);
            transform.localPosition = startPosition + Vector3.up * newPosition;
        }
    }

    public void Begin()
	{ 
        isScrolling = true;
        startTime = Time.time;
    }
}