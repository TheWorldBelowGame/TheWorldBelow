using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour {

	/*public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private bool onGround;
	private Player playerScript;*/
	//public Transform player;
	public GameObject player;
    public Vector3 init_offset;
    public float y_bound;
    public float x_bound;
    Vector3 pos;

    public Camera outside;
    public Camera inside;

    float vel = 0.0f;
    float smoothtime = 0.1f;

    List<float> player_pos;
    bool good = false;
    float count;
    bool left;

	//public  offset;

	// Use this for initialization
	void Start () {
        //playerScript = player.GetComponent<Player> ();
        transform.position = player.transform.position + init_offset;
        player_pos = new List<float>();
        count = 0;
        left = false;
	}
	
    void Update () {

        //player_pos.Add(player.transform.position.x);

        pos = transform.position;

        //pos.x = Mathf.MoveTowards(transform.position.x, player.transform.position.x, .075f);
        //pos.x = Mathf.SmoothDamp(transform.position.x, player_pos[0], ref vel, smoothtime);


        if (player.GetComponent<Rigidbody2D>().velocity.magnitude > 0) {
            if (player.transform.position.x > x_bound + transform.position.x) {
                pos.x = player.transform.position.x - x_bound;
                left = true;
            }
            if (player.transform.position.x < -x_bound + transform.position.x) {
                pos.x = player.transform.position.x + x_bound;
                left = true;
            }
            if (player.transform.position.y > y_bound + transform.position.y) {
                pos.y = player.transform.position.y - y_bound;
                left = true;
            }
            if (player.transform.position.y < -y_bound + transform.position.y) {
                pos.y = player.transform.position.y + y_bound;
                left = true;
            }
            if (pos.x < 1.5f)
                pos.x = 1.5f;
            transform.position = pos;
            count = 0;
        } else if (left) {
            count += Time.deltaTime;
                pos.x = Mathf.MoveTowards(transform.position.x, player.transform.position.x, .1f);
            if (pos.x == player.transform.position.x) {
                left = false;
            }
        }

        if (pos.x < 1.5f)
            pos.x = 1.5f;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            outside.gameObject.SetActive(true);
            inside.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            outside.gameObject.SetActive(false);
            inside.gameObject.SetActive(true);
        }
        

    }

	// Update is called once per frame
	void FixedUpdate () {
		/*//transform.position = new Vector3 (target.position.x, target.position.y + offset.y, target.position.z);
		onGround = playerScript.grounded;

		if (onGround) {
			Vector3 point = GetComponent<Camera> ().WorldToViewportPoint (player.transform.position);
			Vector3 delta = player.transform.position - GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, 0.4f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
		} 
		else { //if not grounded the camera should only follow in the x direction
			Vector3 point = GetComponent<Camera> ().WorldToViewportPoint (player.transform.position);
			Vector3 delta = player.transform.position - GetComponent<Camera> ().ViewportToWorldPoint (new Vector3 (0.5f, point.y, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp (transform.position, destination, ref velocity, dampTime);
		}*/

	}
}
