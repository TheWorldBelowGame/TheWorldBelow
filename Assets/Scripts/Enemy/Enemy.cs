using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float speed = 0.1f;
	public float charge_speed = 3f;
    public float distance = 2;
    public bool start_left = true;
    public bool charge = false;
    public float charge_distance = 5;
    public GameObject button = null;

	public BoxCollider2D col;

    Rigidbody2D rb;
    float start;
	private bool dead = false;
	private bool charging = false;

	public Animator anim;
	enum AnimState { idle, running, death, charge};

	// Use this for initialization
	void Start () {
        start = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        
		Vector3 dir = (start_left ? Vector3.left : Vector3.right);
		Debug.DrawRay (transform.position, dir * charge_distance, Color.yellow);
		RaycastHit2D hit = Physics2D.Raycast (transform.position, dir, charge_distance, 1 << LayerMask.NameToLayer ("Player"));

		if (dead == false) {
			if (charge && hit.collider != null && hit.collider.gameObject.tag == "Player") {
				Vector2 vel = rb.velocity;
				vel.x = (start_left ? -1 : 1) * charge_speed;
				rb.velocity = vel;
				charging = true;
				//Debug.Log ("fast poop");
			} else if (start_left) {
				charging = false;
				if (transform.position.x > start - distance) {
					Vector2 vel = rb.velocity;
					vel.x = -speed;
					rb.velocity = vel;
				} else {
					start = transform.position.x;
					start_left = false;
					Vector3 scale = rb.transform.localScale;
					scale.x *= -1;
					rb.transform.localScale = scale;
				}
			} else {
				if (transform.position.x < start + distance) {
					Vector2 vel = rb.velocity;
					vel.x = speed;
					rb.velocity = vel;
				} else {
					start = transform.position.x;
					start_left = true;
					Vector3 scale = rb.transform.localScale;
					scale.x *= -1;
					rb.transform.localScale = scale;
				}
			}
		}

		// Animation----------------------------------------
		if (dead) {
			anim.SetInteger ("State", (int)AnimState.death);
		} else if (charging) {
			anim.SetInteger ("State", (int)AnimState.charge);
		} else if (rb.velocity.magnitude > 0f) {
			anim.SetInteger ("State", (int)AnimState.running);
		} else if (distance == 0) {
			anim.SetInteger ("State", (int)AnimState.idle);
		} else {
			anim.SetInteger ("State", (int)AnimState.idle);
		}
				
	}

    void OnCollisionEnter2D(Collision2D coll) {
        if (coll.gameObject.tag == "Sword") {
            Global.S.killed++;
			dead = true;
            Destroy(this.gameObject);
        } else if (coll.gameObject.tag == "Wall") {
            start_left = !start_left;
        }
    }

	public void die() {
		dead = true;
		col.enabled = false;
		rb.isKinematic = true;
		Vector2 vel = rb.velocity;
		vel.x = 0f;
		rb.velocity = vel;
        if (button != null) {
            Destroy(button.gameObject);
        }
		//Destroy(gameObject);
	}

}


