using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialoguePlay : State
{
    Sign sign;
    int current;
    int size;

    public DialoguePlay(Sign sign)
	{
        this.sign = sign;
    }

    public override void Start()
	{
		Player.S.playerSM.ChangeState(new PlayerState.Talking());

        if (sign.fall) {
            Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.initOffset.z;
            CameraFollow.S.SetPoiAverage(poi, poi);
        } else {
            CameraFollow.S.SetPoiAverage(Player.S.transform.position, sign.transform.position);
        }
        sign.isBeingRead = true;
        sign.background.gameObject.SetActive(true);
        size = sign.messages.Count;
        if (sign.fall) {
            current = 0;
            sign.dialogue.text = sign.messages[current];
            sign.face.sprite = sign.faces[current];
        } else
            current = -1;
    }

	public override void CheckState() {}

	public override void Update()
	{    
        if (InputManagement.Speak()) {
            current++;
            if (current < size) {
                sign.dialogue.text = sign.messages[current];
                sign.face.sprite = sign.faces[current];
            } else {
                Transition(null);
            }
        }
    }

    public override void Finish()
	{
        sign.background.gameObject.SetActive(false);
        sign.isBeingRead = false;
        if (sign.fall) {
            fade.S.scene = "Main";
            fade.S.fadingOut = true;
            fade.S.changeScene = true;
        } else {
            CameraFollow.S.SetPoiPlayer();
            Player.S.playerSM.ChangeState(new PlayerState.NormalMovement());
        }
    }
}