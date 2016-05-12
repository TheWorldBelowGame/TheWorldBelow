using System;
using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

    //HIDE IN INSPECTOR
    public StateMachine sign_state_machine;

    //PUBLIC
    public List<string> messages;
    public List<Sprite> faces;
    public Text dialogue_go;
    public Image background_go;
    public Image face_go;
    public bool isBeingRead = false;
    bool collided;
    public bool fall = false;
    public GameObject button;

	public Animator anim = null;
	enum AnimState { idle, talking};

    //PRIVATE
    //private bool read = false;
    // Use this for initialization
    void Start() {
        sign_state_machine = new StateMachine();
        collided = false;
        background_go.gameObject.SetActive(false);
        if (button != null)
            button.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (collided && (Input.GetButtonDown(Input_Management.i_Speak) || fall) && !isBeingRead) {
            isBeingRead = true;
            sign_state_machine.ChangeState(new State_Dialogue_Play(this));
        }
        sign_state_machine.Update();

        // Animation
        if (anim != null) {
            if (isBeingRead) {
                anim.SetInteger("State", (int)AnimState.talking);
            } else {
                anim.SetInteger("State", (int)AnimState.idle);
            }
        }
    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collided = true;
            if (button != null)
                button.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collided = false;
            if (button != null)
                button.SetActive(false);
        }
    }
}