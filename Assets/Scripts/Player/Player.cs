﻿using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public static Player S;

	[HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool walled;
    [HideInInspector] public GameObject door;
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
	[HideInInspector] public bool dead = false;

    public StateMachine player_state_machine;
    
	[HideInInspector] public bool pause = false;

    public Vector3 spawn;
	
	/*public enum player_state { STUNNED, NORMAL }
	public player_state current_state = player_state.NORMAL;
    public TextMesh t;
    public string s;*/

	// Use this for initialization
	void Awake () 
	{
        S = this;
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        jumps_left = 0;
        grounded = false;
        walled = false;
        door = null;
        spawn = transform.position;

        Input_Managment.init();
	}
	
	void Start()
	{
        player_state_machine = new StateMachine();
        player_state_machine.ChangeState(new State_Player_Normal_Movement(this));
	}

	void Update() {
        player_state_machine.Update();
		if (Input.GetButtonDown(Input_Managment.i_Start)){
			SceneManager.LoadScene (0);
		}
        if (dead) {
            if (!fade.S.fadingOut)
                die();
        }
    }

	void FixedUpdate()
	{
		
	}


    // Checking if the player is grounded and can jump -----------------
    void OnCollisionEnter2D(Collision2D coll) {
        switch (coll.gameObject.tag) {
            case "Ground":
                grounded = true;
                jumps_left = 1;
                //Debug.Log ("grounded");
                break;
            case "Hidden_Platform":
                grounded = true;
                jumps_left = 1;
                //Debug.Log ("grounded");
                break;
            case "Wall":
                walled = true;
                break;
		    case "Enemy":
			    dead = true;
                fade.S.fadingOut = true;
                //die();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Wall") {
            walled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == ("Door")) {
            door = trigger.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.gameObject.tag == ("Door")) {
            door = null;
        }
    }

    void die() {
        transform.position = spawn;
        Vector3 scale = rb2d.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        rb2d.transform.localScale = scale;
        dead = false;
        Invoke("undie", 0.5f);
    }

    void undie() {
        fade.S.fadingIn = true;
    }
}
