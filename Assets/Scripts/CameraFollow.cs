using UnityEngine;
using System.Collections;

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

	//public  offset;

	// Use this for initialization
	void Start () {
        //playerScript = player.GetComponent<Player> ();
        transform.position = player.transform.position + init_offset;
	}
	
    void Update () {
        pos = transform.position;
        if ( player.transform.position.x > x_bound + transform.position.x) {
            pos.x = player.transform.position.x - x_bound;
        }
        if (player.transform.position.x < -x_bound + transform.position.x) {
            pos.x = player.transform.position.x + x_bound;
        }
        if (player.transform.position.y > y_bound + transform.position.y) {
            pos.y = player.transform.position.y - y_bound;
        }
        if (player.transform.position.y < -y_bound + transform.position.y) {
            pos.y = player.transform.position.y + y_bound;
        }
        transform.position = pos;
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
