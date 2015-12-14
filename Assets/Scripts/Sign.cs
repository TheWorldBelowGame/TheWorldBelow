using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Sign : MonoBehaviour {

    //HIDE IN INSPECTOR
    [HideInInspector]
    public List<Element> _elementQueue = new List<Element>();

    //PUBLIC
    public List<string> messages;
    public Canvas canvas;

    //PRIVATE
    private bool read = false;
    // Use this for initialization
    void Start() {
        Element.addElement(_elementQueue, new DialogueDeactivate(canvas));
        Element.addElement(_elementQueue, new DialogueWaiting(_elementQueue, messages, canvas));
    }

    // Update is called once per frame
    void Update() {
        Element.updateQueue(_elementQueue);
    }

    void FixedUpdate() {
        
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player") && Input.GetButtonDown("Submit")) {
            Element.trigger2DQueue(_elementQueue, other);
        }
    }
}