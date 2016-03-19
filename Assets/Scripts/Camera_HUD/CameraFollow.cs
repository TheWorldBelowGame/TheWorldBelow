using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow S;
    
	public GameObject player;
    public Vector3 init_offset;
    Vector3 pos;

    public Camera outside;
    public Camera inside;

    public float speed = 0.1f;

    bool outside_on;

    void Awake() {
        S = this;
    }

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + init_offset;
        outside_on = true;
        outside.gameObject.SetActive(true);
        inside.gameObject.SetActive(false);
    }
	
    void FixedUpdate () {

        pos = transform.position;
        
        if (player.transform)
            pos = Vector3.Lerp(transform.position, player.transform.position + init_offset, speed);

        pos.x = (pos.x < 1.5f ? 1.5f : pos.x);

        transform.position = pos;

    }

    void LateUpdate() {
        /*pos = transform.position;
        if (player.transform.position.y > y_bound + pos.y) {
            pos.y = player.transform.position.y - y_bound;
        } else if (player.transform.position.y < -y_bound + pos.y) {
            pos.y = player.transform.position.y + y_bound;
        }
        transform.position = pos;*/
    }

    public void in_out() {
        if (outside_on) {
            outside.gameObject.SetActive(false);
            inside.gameObject.SetActive(true);
        } else {
            outside.gameObject.SetActive(true);
            inside.gameObject.SetActive(false);
        }
        outside_on = ! outside_on;
    }
}
