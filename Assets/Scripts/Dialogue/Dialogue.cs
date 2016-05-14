using System;
using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
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
	
    void Start()
	{
        background.gameObject.SetActive(false);
    }

	public void StartReading()
	{
		anim.SetInteger("State", kAnimTalking);
		isBeingRead = true;
		background.gameObject.SetActive(true);
		size = messages.Count;

		if (fall) {
			current = 0;
			dialogue.text = messages[current];
			face.sprite = faces[current];
			Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.initOffset.z;
			CameraFollow.S.SetPoiAverage(poi, poi);
		} else {
			current = -1;
			CameraFollow.S.SetPoiAverage(Player.S.transform.position, transform.position);
		}
	}

	// Advances the dialogue one step
	public void Advance()
	{
		current++;
		if (current < size) {
			dialogue.text = messages[current];
			face.sprite = faces[current];
		} else {
			background.gameObject.SetActive(false);
			isBeingRead = false;
			if (fall) {
				Fade.S.WhenDone("Main");
				Fade.S.FadeOut();
			} else {
				CameraFollow.S.SetPoiPlayer();
				Player.S.playerSM.ChangeState(new PlayerState.NormalMovement());
			}
			anim.SetInteger("State", kAnimIdle);
		}
	}
}