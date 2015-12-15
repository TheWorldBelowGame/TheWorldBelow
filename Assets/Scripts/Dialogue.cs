using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class Dialogue : MonoBehaviour {

    //HIDE IN INSPECTOR
    [HideInInspector]
    public List<Element> _elementQueue = new List<Element>();

    //PUBLIC
    public Canvas canvas;

    //PRIVATE

    // Use this for initialization
    void Start() {
        Element.addElement(_elementQueue, new DialogueDeactivate(canvas));
    }

    // Update is called once per frame
    void Update() {
        Element.updateQueue(_elementQueue);
        /*if (Input.GetKeyDown(KeyCode.L)) {
            Element.addElement(_elementQueue, new DialogueActivate(canvas));
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            Element.addElement(_elementQueue, new DialogueDeactivate(canvas));
        }*/
    }

    void FixedUpdate() {
        
    }
}