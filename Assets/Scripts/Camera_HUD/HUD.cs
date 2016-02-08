using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        text.text = "Collected: " + Global.S.collected + "\nKilled: " + Global.S.killed;
	}
}
