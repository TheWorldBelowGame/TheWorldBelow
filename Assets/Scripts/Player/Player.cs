﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
	// Singleton
	[HideInInspector] public static Player S;
	[HideInInspector] public StateMachine<PlayerState.BasePlayerState> playerSM;

	// Public
	[HideInInspector] public GameObject door;
	[HideInInspector] public Animator anim;
	[HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool walled;
	[HideInInspector] public bool pause = false;
	[HideInInspector] public Dialogue dialogue;

	// Visible in Editor
	public GameObject sword;
    public Vector3 spawn;

	// Private
	public void UseDoor()
	{
		CameraFollow.InOut();
		door.GetComponent<Door>().InOut();
		spawn = transform.position;
	}
	
	void Awake() 
	{
        S = this;
		playerSM = new StateMachine<PlayerState.BasePlayerState>(new PlayerState.NormalMovement());
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        grounded = false;
        walled = false;
        door = null;
		dialogue = null;
        spawn = transform.position;

		InputManagement.init();
	}
	
	void Start()
	{
		sword.SetActive(false);
		playerSM.ChangeState(new PlayerState.NormalMovement());
	}

	void Update()
	{
		if (InputManagement.Start()) {
			SceneManager.LoadScene(0);
		}
		
		playerSM.Update();
	}

    // Checking if the player is grounded and can jump
    void OnCollisionEnter2D(Collision2D coll) {
        switch (coll.gameObject.tag) {
            case "Ground":
            case "Hidden_Platform":
                grounded = true;
                break;
            case "Wall":
                walled = true;
                break;
		    case "Enemy":
				playerSM.ChangeState(new PlayerState.Dying());
                break;
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
		switch (coll.gameObject.tag) {
			case "Wall":
				walled = false;
				break;
		}
    }

    void OnTriggerEnter2D(Collider2D coll) {
		switch (coll.tag) {
			case "Door":
				door = coll.gameObject;
				break;
			case "Dialogue trigger":
				dialogue = coll.gameObject.GetComponent<Dialogue>();
				break;
			case "FallingDialogue":
				rb2d.isKinematic = true;
				dialogue = coll.gameObject.GetComponent<Dialogue>();
				dialogue.StartReading();
				break;
		}
    }

    void OnTriggerExit2D(Collider2D coll) {
		switch (coll.tag) {
			case "Door":
				door = null;
				break;
			case "Dialogue trigger":
				dialogue = null;
				break;
		}
	}

	public void Fall()
	{
		playerSM.ChangeState(new PlayerState.Falling());
	}
}
