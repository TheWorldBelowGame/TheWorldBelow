using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;

class DialogueEngine : MonoBehaviour
{
	// Singleton
	static DialogueEngine S;

	// Visible in Editor
	public Text dialogueObj;
	public Image backgroundObj;
	public Image faceObj;

	// Private
	static Action onFinish;
	static List<string> messages;
	static List<Sprite> faces;
	static int current;
	static bool inUse;

	// Loads dialogue for the specified actor, and begins to display the dialogue
	// Optional parameter allows a callback function when dialogue is done
	public static void Begin(string actorName, Action _onFinish = null)
	{
		if (inUse) {
			Debug.Log("Tried to start dialogue while DialogueEngine was in use!");
			return;
		}

		// Load messages and faces
		StreamReader dialogueLines = new StreamReader(UnityEngine.Resources.Load<TextAsset>(actorName + ".txt").text);
		string readText;
		while ((readText = dialogueLines.ReadLine()) != null) {
			messages.Add(readText);
		}

		inUse = true;
		onFinish = _onFinish;
		S.backgroundObj.gameObject.SetActive(true);
		current = -1;
		Advance();
	}

	// Displays the next line of dialogue.
	// Returns false if there is no dialogue remaining
	public static bool Advance()
	{
		current++;

		if (current < messages.Count) {
			S.dialogueObj.text = messages[current];
			S.faceObj.sprite = faces[current];
		} else {
			inUse = false;
			if (onFinish != null) {
				onFinish();
			}
		}

		return inUse;
	}
	
	void Start()
	{
		S = this;
		inUse = false;
		backgroundObj.gameObject.SetActive(false);
	}


}
