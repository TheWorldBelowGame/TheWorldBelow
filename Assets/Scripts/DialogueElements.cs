using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueActivate : Element {
    Canvas canvas;

    public DialogueActivate(Canvas canvas) {
        this.canvas = canvas;
    }

    public override void onActive() {
        canvas.enabled = true;
        Time.timeScale = 0;
    }

    public override void update() {
        finished = true;
    }

    public override void onRemove() {
        
    }
}

public class DialogueWaiting : Element {
    List<Element> _elementQueue;
    List<string> text;
    Canvas canvas;

    public DialogueWaiting(List<Element> _elementQueue, List<string> text, Canvas canvas) {
        this._elementQueue = _elementQueue;
        this.text = text;
        this.canvas = canvas;
        
    }

    public override void onActive() {
        
    }

    public override void update() {
        
    }

    public override void onRemove() {

    }

    public override void onTrigger2D(Collider2D coll) {
        addElement(_elementQueue, new DialogueActivate(canvas));
        addElement(_elementQueue, new DialogueText(text, canvas));
        addElement(_elementQueue, new DialogueDeactivate(canvas));
        addElement(_elementQueue, new DialogueWaiting(_elementQueue, text, canvas));
        finished = true;
    }
}

public class DialogueDeactivate : Element {
    Canvas canvas;

    public DialogueDeactivate(Canvas canvas) {
        this.canvas = canvas;
    }

    public override void onActive() {
        canvas.enabled = false;
        canvas.GetComponentInChildren<Text>().text = "";
        Time.timeScale = 1;
    }

    public override void update() {
        finished = true;
    }

    public override void onRemove() {

    }
}

public class DialogueText : Element {
    List<string> text;
    Canvas canvas;
    int current;
    int size;

    public DialogueText(List<string> text, Canvas canvas) {
        this.text = text;
        this.canvas = canvas;
    }

    public override void onActive() {
        size = text.Count;
        current = 0;
        if (size > 0) {
            canvas.GetComponentInChildren<Text>().text = text[current];
        }
        else {
            finished = true;
        }
    }

    public override void update() {
        if (Input.GetButtonDown("Submit")) {
            current++;
            if (current < size) {
                canvas.GetComponentInChildren<Text>().text = text[current];
            }
            else {
                finished = true;
            }
        }
    }

    public override void onRemove() {

    }
}