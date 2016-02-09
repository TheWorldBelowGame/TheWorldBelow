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
    public Text dialogue;
    public bool isBeingRead = false;
    bool collided;

    //PRIVATE
    //private bool read = false;
    // Use this for initialization
    void Start() {
        sign_state_machine = new StateMachine();
        collided = false;
    }

    // Update is called once per frame
    void Update() {
        if (collided && Input.GetButtonDown("Submit") && !isBeingRead) {
            isBeingRead = true;
            sign_state_machine.ChangeState(new State_Dialogue_Play(this, messages, dialogue));
        }
        sign_state_machine.Update();
    }

    void FixedUpdate() {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collided = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            collided = false;
        }
    }
}