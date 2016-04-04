using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow S;
    
	public GameObject player;
    public Vector3 init_offset;

    public bool falling = false;

    Vector3 poi;
    Vector3 pos;

    public Camera outside;
    public Camera inside;

    public float speed = 0.1f;

    bool outside_on;
    bool dialouge;

    void Awake() {
        S = this;
    }

	// Use this for initialization
	void Start () {
        transform.position = player.transform.position + init_offset;
        poi = player.transform.position + init_offset;
        outside_on = true;
        outside.gameObject.SetActive(true);
        inside.gameObject.SetActive(false);
        dialouge = false;
    }
	
    void FixedUpdate () {

        pos = transform.position;
        
        if(!dialouge)
            poi = player.transform.position + init_offset;

        pos = Vector3.Lerp(transform.position, poi, speed);

        pos.x = (pos.x < 1.5f ? 1.5f : pos.x);

		//ParallaxBackground.S.Move(Vector3.Distance(pos, transform.position));
        transform.position = pos;

    }

    void LateUpdate() {
        if (falling) {
            pos = transform.position;
            //if (transform.position.y - init_offset.y > player.transform.position.y)
                pos.y = player.transform.position.y;
            transform.position = pos;
        }
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

    public void set_poi_player() {
        poi = player.transform.position + init_offset;
        inside.orthographicSize = 5;
        dialouge = false;
    }

    public void set_poi_average(Vector3 go1, Vector3 go2) {
        dialouge = true;
        poi.x = (go1.x + go2.x) / 2;
        poi.y = (go1.y + go2.y) / 2 - 0.5f;
        poi.z = -10;
        inside.orthographicSize = 3;
    }
}
