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
        if (s.fall) {
            //Player.S.player_state_machine.ChangeState(new State_Player_Falling());
        } else {
            Player.S.player_state_machine.ChangeState(new State_Player_Paused());
        }
		Player.S.rb2d.velocity = (new Vector2 (0, 0));
		Player.S.anim.SetInteger ("State", (int)AnimState.idle);
        if (s.fall) {
            Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.init_offset.z;
            CameraFollow.S.set_poi_average(poi, poi);
        } else {
            CameraFollow.S.set_poi_average(Player.S.transform.position, s.transform.position);
        }
        s.isBeingRead = true;
        s.background_go.gameObject.SetActive(true);
        size = s.messages.Count;
        if (s.fall) {
            current = 0;
            s.dialogue_go.text = s.messages[current];
            s.face_go.sprite = s.faces[current];
        } else
            current = -1;
    }

    public override void OnUpdate() {
        
        if (Input.GetButtonDown(Input_Management.i_Speak)) {
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
        if (s.fall) {
            fade.S.scene = "Main";
            fade.S.fadingOut = true;
            fade.S.changeScene = true;
        } else {
            CameraFollow.S.set_poi_player();
            Player.S.player_state_machine.ChangeState(new State_Player_Normal_Movement(Player.S));
        }
    }
}