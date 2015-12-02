using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;
	public float moveForce = 365f;
	//public float maxSpeed = 5f;
	public float jumpForce = 1000f;
	public float jumpTime = 1f;

	
	private bool grounded;
	private float timer;
	//private Animator anim;
	private Rigidbody2D rb2d;
	
	
	// Use this for initialization
	void Awake () 
	{
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		timer = jumpTime;
		//gameObject.layer = 8;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Physics.IgnoreLayerCollision (8, 9, true);

		if (Input.GetButtonDown ("Jump") && grounded) {
			jump = true;
			grounded = false;
		}
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis("Horizontal");
		float jumpmove;

		if (jump) {
			// Add a vertical force to the player.
			if (timer > 0) {
				rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
				timer -= Time.deltaTime;
			} else {
				// Make sure the player can't jump again until the jump conditions from Update are satisfied.
				jump = false;
				timer = jumpTime;
			}
		}

		if (grounded) {
			jumpmove = 1f;
		} else {
			jumpmove = 0.75f;
		}

		// Left and right movement
		rb2d.velocity = (new Vector2(h * moveForce * jumpmove, rb2d.velocity.y));

		//-----ANIMATION STUFF-----
		//anim.SetFloat("Speed", Mathf.Abs(h));
	}
	
	
	/*void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}*/

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
		}
	}
}

