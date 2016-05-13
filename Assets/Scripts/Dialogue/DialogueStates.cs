using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialoguePlay : State
{
    Sign s;
    int current;
    int size;

    public DialoguePlay(Sign s)
	{
        this.s = s;
    }

    public override void Start()
	{
		Player.S.playerSM.ChangeState(new PlayerState.Talking());

        if (s.fall) {
            Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.init_offset.z;
            CameraFollow.S.set_poi_average(poi, poi);
        } else {
            CameraFollow.S.set_poi_average(Player.S.transform.position, s.transform.position);
        }
        s.isBeingRead = true;
        s.background.gameObject.SetActive(true);
        size = s.messages.Count;
        if (s.fall) {
            current = 0;
            s.dialogue.text = s.messages[current];
            s.face.sprite = s.faces[current];
        } else
            current = -1;
    }

	public override void CheckState() {}

	public override void Update()
	{    
        if (InputManagement.Speak()) {
            current++;
            if (current < size) {
                s.dialogue.text = s.messages[current];
                s.face.sprite = s.faces[current];
            } else {
                Transition(null);
            }
        }
    }

    public override void Finish()
	{
        s.background.gameObject.SetActive(false);
        s.isBeingRead = false;
        if (s.fall) {
            fade.S.scene = "Main";
            fade.S.fadingOut = true;
            fade.S.changeScene = true;
        } else {
            CameraFollow.S.set_poi_player();
            Player.S.playerSM.ChangeState(new PlayerState.NormalMovement());
        }
    }
}