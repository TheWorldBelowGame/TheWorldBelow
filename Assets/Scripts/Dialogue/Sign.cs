using System;
using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
	const int kAnimIdle = 0;
	const int kAnimTalking = 1;

    public List<string> messages;
    public List<Sprite> faces;
    public Text dialogue;
    public Image background;
    public Image face;
    public bool isBeingRead = false;
    public bool fall = false;
	public Animator anim = null;

	int current;
	int size;

	public void StartReading()
	{
		if (fall) {
			Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.initOffset.z;
			CameraFollow.S.SetPoiAverage(poi, poi);
		} else {
			CameraFollow.S.SetPoiAverage(Player.S.transform.position, transform.position);
		}

		isBeingRead = true;
		background.gameObject.SetActive(true);
		size = messages.Count;
		if (fall) {
			current = 0;
			dialogue.text = messages[current];
			face.sprite = faces[current];
		} else {
			current = -1;
		}
	}
	
    void Start()
	{
        background.gameObject.SetActive(false);
    }
	
    void Update()
	{
		if (InputManagement.Speak()) {
			current++;
			if (current < size) {
				dialogue.text = messages[current];
				face.sprite = faces[current];
			} else {
				background.gameObject.SetActive(false);
				isBeingRead = false;
				if (fall) {
					fade.S.scene = "Main";
					fade.S.fadingOut = true;
					fade.S.changeScene = true;
				} else {
					CameraFollow.S.SetPoiPlayer();
					Player.S.playerSM.ChangeState(new PlayerState.NormalMovement());
				}
			}
		}

		if (anim != null) {
            if (isBeingRead) {
                anim.SetInteger("State", kAnimTalking);
            } else {
                anim.SetInteger("State", kAnimIdle);
            }
        }
    }
}