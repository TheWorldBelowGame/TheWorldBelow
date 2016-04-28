using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Gui_fade : MonoBehaviour {

	Color startColor = new Color (0,0,0,0);
	public Color endColor;
	public float duration;
	float t;

	public float delay = 0;
	float i;
	bool done;
	bool start;

	Text txt;

	// Use this for initialization
	void Start () {
		txt = GetComponent<Text>();
		txt.color = startColor;
		t = 0;
		i = 0;
		done = false;
		start = false;
	}
	
	// Update is called once per frame
	void Update () {
		i += Time.deltaTime;

		if (i >= delay) {
			start = true;
		}

		if(start && !done)
			fade ();
	}

	void fade(){
		//print (t / duration);
		txt.color = Color.Lerp(startColor, endColor, (t) / duration);
		//print (txt.color);
		t += Time.deltaTime;
		if (txt.color.a > 0.95) {
			txt.color = endColor;
			done = true;
		}
	}

}
