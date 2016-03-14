﻿using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

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

    public StateMachine player_state_machine;
    
	[HideInInspector] public bool pause = false;
	
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
		//anim.SetBool("Facing left", false);
		//gameObject.layer = 8;
	}
	
	void Start()
	{
        player_state_machine = new StateMachine();
        player_state_machine.ChangeState(new State_Player_Normal_Movement(this));
	}

	void Update() {
        player_state_machine.Update();
		if (Input.GetButtonDown("Start")){
			SceneManager.LoadScene (0);
		}
    }

	void FixedUpdate()
	{
		
	}


	// Checking if the player is grounded and can jump -----------------
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground") {
			grounded = true;
            jumps_left = 1;
			//Debug.Log ("grounded");
		}
	}


	// Checking if the play has entered a trigger zone -----------------
	void OnTriggerStay2D(Collider2D trigger) {
		if (trigger.gameObject.tag == ("Scene trigger")) {
			Debug.Log ("poop");
			if (Input.GetButtonDown ("X Button")) {
				//scene
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