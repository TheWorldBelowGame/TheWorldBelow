﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Generic dialogue behavior. Anything with dialogue implements this script.
public class Dialogue : MonoBehaviour
{
	// Definitions
	const int kAnimIdle = 0;
	const int kAnimTalking = 1;

	// Public
	[HideInInspector] public bool isBeingRead = false;

	// Visible in Editor
	public List<string> messages;
    public List<Sprite> faces;
    public Text dialogue;
    public Image background;
    public Image face;
    public bool fall = false;
	public Animator anim = null;

	// Private
	int current;
	int size;
	
    void Start()
	{
        background.gameObject.SetActive(false);
    }

	public void StartReading()
	{
		if (anim != null) {
			anim.SetInteger("State", kAnimTalking);
		}

		isBeingRead = true;
		background.gameObject.SetActive(true);
		size = messages.Count;

		if (fall) {
			current = 0;
			dialogue.text = messages[current];
			face.sprite = faces[current];
			//Vector3 poi = Player.S.transform.position + Vector3.forward * CameraFollow.S.initOffset.z;
			//CameraFollow.S.SetPoiAverage(poi, poi);
		} else {
			current = -1;
		}
		
		CameraFollow.S.SetPoiAverage(Player.S.transform.position, transform.position);
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
				Fade.FadeOut(1f, "Main");
			} else {
				CameraFollow.S.SetPoiPlayer();
				Player.S.playerSM.ChangeState(new PlayerState.NormalMovement());
			}
			anim.SetInteger("State", kAnimIdle);
		}
	}
}