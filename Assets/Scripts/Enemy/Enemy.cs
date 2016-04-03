using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed = 0.1f;
    public float distance = 2;
    public bool start_left = true;
    public bool charge = false;
    public float charge_distance = 5;

    Rigidbody2D rb;
    float start;

	// Use this for initialization
	void Start () {
        start = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        
        Vector3 dir = (start_left ? Vector3.left : Vector3.right);
        Debug.DrawRay(transform.position, dir * charge_distance, Color.yellow);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, charge_distance, 1 << LayerMask.NameToLayer("Player"));
        if (charge && hit.collider != null && hit.collider.gameObject.tag == "Player") {
            Vector2 vel = rb.velocity;
            vel.x = (start_left ? -1 : 1) * speed;
            rb.velocity = vel;
        } else if (start_left) {
            if (transform.position.x > start - distance) {
                Vector2 vel = rb.velocity;
                vel.x = -speed;
                rb.velocity = vel;
            } else {
                start = transform.position.x;
                start_left = false;
            }
        } else {
            if (transform.position.x < start + distance) {
                Vector2 vel = rb.velocity;
                vel.x = speed;
                rb.velocity = vel;
            } else {
                start = transform.position.x;
                start_left = true;
            }
        }

        
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Sword") {
            Global.S.killed++;
            Destroy(this.gameObject);
        } else if (coll.gameObject.tag == "Wall") {
            start_left = !start_left;
        }
    }

    public void die() {
        Destroy(gameObject);
    }
}
