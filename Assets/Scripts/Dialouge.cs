using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementDialouge : Element
{
	
	public ElementDialouge(){
	}
	
	public override void onActive(){
		Time.timeScale = 0;

	}
	
	public override void update() {
		//PAUSE MENU
	}
	
	public override void onRemove() {
		Time.timeScale = 1;
	}
}