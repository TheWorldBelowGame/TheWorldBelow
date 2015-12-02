using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementLoadScene : Element
{
	string scene_name;
	
	public ElementLoadScene(string scene_name)
	{ 
		this.scene_name = scene_name;
	}
	
	public override void onActive()
	{
		Application.LoadLevel(scene_name);
	}
}
