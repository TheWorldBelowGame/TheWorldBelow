using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    public static Player S;

	[HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool grounded;
    [HideInInspector] public int jumps_left;
	public float moveForce = 365f;
	public float runForce = 500f;
	public float jumpForce = 1000f;
	public float jumpTime = 1f;
	public bool doubleJump = true;
    public float attack_speed = 0.25f;

	public Animator anim;

    public GameObject sword;

	//private Animator anim;
	[HideInInspector] public Rigidbody2D rb2d;

	[HideInInspector] public List<Element> _elementQueue = new List<Element>();
	[HideInInspector] public bool pause = false;
	
	/*public enum player_state { STUNNED, NORMAL }
	public player_state current_state = player_state.NORMAL;
    public TextMesh t;
    public string s;*/

	// Use this for initialization
	void Awake () 
	{
        S = this;
		//anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        jumps_left = 0;
        grounded = false;
		anim.SetBool("Facing left", false);
		//gameObject.layer = 8;
	}
	
	void Start()
	{
		//Element.addElement(_elementQueue, new ElementSetTextMesh(t, s));
		//Element.addElement(_elementQueue, new ElementMoveOverTime(120, new Vector3(0f, 0.0f, 0.0f), new Vector3(-10, 0.0f, 0.0f), gameObject, false));
		Element.addElement(_elementQueue, new ElementStandardControl(this));
	}

	void Update() {
        /*if (!pause && Input.GetButtonDown ("Start")) {
			Time.timeScale = 0;
			Element.insertQueue(_elementQueue, new ElementPause());
			//pause = true;
		}
		if (pause && Input.GetButtonDown ("Start")) {
			Time.timeScale = 1;
			Element.removeQueue(_elementQueue);
			//pause = false;
		}
		if (Time.timeScale > 0) {
			pause = false;
		} else {
			pause = true;
		}*/
        Element.updateQueue(_elementQueue);
    }

	void FixedUpdate()
	{
		//Element.updateQueue(_elementQueue);
	}


	// Checking if the player is grounded and can jump -----------------
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
            jumps_left = 2;
			//Debug.Log ("grounded");
		}
	}


	// Checking if the play has entered a trigger zone -----------------
	void OnTriggerStay2D(Collider2D trigger) {
		if (trigger.gameObject.tag == "Dialogue trigger") {
			if (Input.GetButtonDown ("X Button")) {
				//Element.insertQueue(_elementQueue, new ElementDialouge());
			}
		}
		if (trigger.gameObject.tag == ("Scene trigger")) {
			Debug.Log ("poop");
			if (Input.GetButtonDown ("X Button")) {
				Element.addElement(_elementQueue, new ElementLoadScene(trigger.gameObject.name));
				Element.removeQueue(_elementQueue);
			}
		}
	}

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "Collectable") {
            Global.S.collected++;
            Destroy(trigger.gameObject);
        }
    }
}
