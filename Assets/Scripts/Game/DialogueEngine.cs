using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections.Generic;
using System;

class DialogueEngine : MonoBehaviour
{
	// Private
	static Image backgroundObj;
	static Text dialogueObj;
	static Image faceObj;
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
		messages = new List<string>();
		faces = new List<Sprite>();
		
		TextAsset dialogueFile = UnityEngine.Resources.Load<TextAsset>("Dialogue/" + actorName);
		if (dialogueFile == null) {
			Debug.Log("Unable to load file: " + actorName);
			return;
		}

		StreamReader dialogueLines = new StreamReader(new MemoryStream(dialogueFile.bytes));
		string readText;
		while ((readText = dialogueLines.ReadLine()) != null) {
			messages.Add(readText);
		}

		inUse = true;
		onFinish = _onFinish;
		backgroundObj.gameObject.SetActive(true);
		current = -1;
		Advance();
	}

	// Displays the next line of dialogue.
	// Returns false if there is no dialogue remaining
	public static bool Advance()
	{
		current++;

		if (current < messages.Count) {
			dialogueObj.text = messages[current];
			faceObj.sprite = faces[current];
		} else {
			backgroundObj.gameObject.SetActive(false);
			inUse = false;
			if (onFinish != null) {
				onFinish();
			}
		}

		return inUse;
	}
	
	void Start()
	{
		inUse = false;
		backgroundObj = GameObject.Find("/Canvas/Background").GetComponent<Image>();
		dialogueObj = GameObject.Find("/Canvas/Background/Dialogue").GetComponent<Text>();
		faceObj = GameObject.Find("/Canvas/Background/Face").GetComponent<Image>();

		backgroundObj.gameObject.SetActive(false);
	}
}
