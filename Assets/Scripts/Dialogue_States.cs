using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Dialogue_Play : State {
    Sign s;
    List<string> messages;
    Text dialogue;
    int current;
    int size;

    public State_Dialogue_Play(Sign s, List<string> messages, Text dialogue) {
        this.messages = messages;
        this.dialogue = dialogue;
        this.s = s;
    }

    public override void OnStart() {
        s.isBeingRead = true;
        dialogue.gameObject.SetActive(true);
        size = messages.Count;
        current = -1;
    }

    public override void OnUpdate(float time_delta_fraction) {
        
        if (Input.GetButtonDown("Submit")) {
            current++;
            if (current < size) {
                dialogue.text = messages[current];
            }
            else {
                ConcludeState();
            }
        }
    }

    public override void OnFinish() {
        dialogue.gameObject.SetActive(false);
        s.isBeingRead = false;
    }
}