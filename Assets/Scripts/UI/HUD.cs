using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Controls the player HUD and updates any information there
public class HUD : MonoBehaviour
{
	// Visible in Editor
    public Text numOrbs;
	
	void Start()
	{
	
	}
	
	void Update()
	{
		numOrbs.text =  (10 - Global.S.collected).ToString();
	}
}
