using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class State_Dialogue_Play : State {
    Sign s;
    int current;
    int size;

    public State_Dialogue_Play(Sign s) {
        this.s = s;
    }

    public override void OnStart() {
        Player.S.player_state_machine.ChangeState(new State_Player_Paused());
		Player.S.rb2d.velocity = (new Vector2 (0, 0));
		Player.S.anim.SetInteger ("State", (int)AnimState.idle);
        CameraFollow.S.set_poi_average(Player.S.transform.position, s.transform.position);
        s.isBeingRead = true;
        s.background_go.gameObject.SetActive(true);
        size = s.messages.Count;
        current = -1;
    }

    public override void OnUpdate(float time_delta_fraction) {
        
        if (Input.GetButtonDown("Submit")) {
            current++;
            if (current < size) {
                s.dialogue_go.text = s.messages[current];
                s.face_go.sprite = s.faces[current];
            }
            else {
                ConcludeState();
            }
        }
    }

    public override void OnFinish() {
        s.background_go.gameObject.SetActive(false);
        s.isBeingRead = false;
        if(s.fall) {
            SceneManager.LoadScene("Test_03");
        }
        CameraFollow.S.set_poi_player();
        Player.S.player_state_machine.ChangeState(new State_Player_Normal_Movement(Player.S));
    }
}