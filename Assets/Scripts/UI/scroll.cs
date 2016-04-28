using UnityEngine;
using System.Collections;

public class scroll : MonoBehaviour {
    public float scrollSpeed;
    public float tileSizeZ;

    bool s;

    private Vector3 startPosition;

    float startTime;

    void Start() {
        s = false;
        startPosition = transform.localPosition;
    }

    void Update() {
        if (s) {
            float newPosition = Mathf.Repeat((Time.time - startTime) * scrollSpeed, tileSizeZ);
            transform.localPosition = startPosition + Vector3.up * newPosition;
        }
    }

    public void begin() {
        s = true;
        startTime = Time.time;
    }
}