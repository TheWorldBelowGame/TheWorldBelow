using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	enum AnimState { idle, running, death, charge };

	public float speed = 0.1f;
	public float chargeSpeed = 3f;
    public float distance = 2;
    public bool startLeft = true;
    public bool charge = false;
    public float chargeDistance = 5;
    public GameObject button = null;
	public BoxCollider2D col;
	public Animator anim;

	Rigidbody2D rb;
    float start;
	bool dead = false;
	bool charging = false;
	
	void Start ()
	{
        start = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		Vector3 dir = startLeft ? Vector3.left : Vector3.right;
		Debug.DrawRay(transform.position, dir * chargeDistance, Color.yellow);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, chargeDistance, 1 << LayerMask.NameToLayer ("Player"));

		if (!dead) {
			if (charge && hit.collider != null && hit.collider.gameObject.tag == "Player") {
				Vector2 vel = rb.velocity;
				vel.x = (startLeft ? -1 : 1) * chargeSpeed;
				rb.velocity = vel;
				charging = true;
			} else if (startLeft) {
				charging = false;
				if (transform.position.x > start - distance) {
					Vector2 vel = rb.velocity;
					vel.x = -speed;
					rb.velocity = vel;
				} else {
					start = transform.position.x;
					startLeft = false;
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
					startLeft = true;
					Vector3 scale = rb.transform.localScale;
					scale.x *= -1;
					rb.transform.localScale = scale;
				}
			}
		}

		// Animation
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

    void OnCollisionEnter2D(Collision2D coll)
	{
        if (coll.gameObject.tag == "Sword") {
            Global.S.killed++;
			dead = true;
            Destroy(this.gameObject);
        } else if (coll.gameObject.tag == "Wall") {
            startLeft = !startLeft;
        }
    }

	public void Die()
	{
		dead = true;
		col.enabled = false;
		rb.isKinematic = true;
		Vector2 vel = rb.velocity;
		vel.x = 0f;
		rb.velocity = vel;
        if (button != null) {
            Destroy(button.gameObject);
        }
	}

}


