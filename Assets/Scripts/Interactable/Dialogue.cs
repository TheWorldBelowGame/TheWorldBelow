using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

// Generic dialogue behavior. Anything with dialogue implements this script.
public class Dialogue : MonoBehaviour, Interactable
{
	// Definitions
	public enum AnimState { Idle, Talking }

	// Visible in Editor
	public Animator anim;
	public string actorName;

	public void Interact()
	{
		anim.SetInteger("State", AnimState.Talking.ToInt());
		CameraFollow.FocusBetweenPlayerAndPoint(transform.position);
		DialogueEngine.Begin(actorName, DoneReading);
	}

	void DoneReading()
	{
		anim.SetInteger("State", AnimState.Idle.ToInt());
		CameraFollow.FocusPlayer();
	}
}