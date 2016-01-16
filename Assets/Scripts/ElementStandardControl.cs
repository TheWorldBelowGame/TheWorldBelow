using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementStandardControl : Element
{
	Player player;
	private bool jump;
	private bool running;
	private float timer;

	
	public ElementStandardControl(Player player)
	{
		this.player = player;
	}
	
	public override void onActive()
	{
		//player.GetComponent<SimplePlatformController>().enabled = true;
		timer = player.jumpTime;
		
	}
	
	public override void update() {

		//if (Input.GetButtonDown ("Jump") && player.grounded) {
		if (Input.GetButtonDown ("Jump")) {
            //Debug.Log("jump");
			jump = true;
			//player.grounded = false;
		}

		float h = Input.GetAxis("Horizontal");
		float jumpmove;

		// Seeing if the player is running or walking
		if (Input.GetButton ("B Button") && player.grounded) {
			running = true;
			//Debug.Log ("running");
		} else {
			running = false;
			//Debug.Log ("walking");
		}
		
		if (player.grounded && running) {
			jumpmove = 2f;
		} else {
			jumpmove = 1f;
		}

		// Jumping
		if (jump) {
			// Add a vertical force to the player.
			if (timer > 0) {
				player.rb2d.AddForce(new Vector2(0, player.jumpForce), ForceMode2D.Impulse);
				timer -= Time.deltaTime;
			} else {
				// Make sure the player can't jump again until the jump conditions from Update are satisfied.
				jump = false;
				timer = player.jumpTime;
			}
		}


		
		// Left and right walk movement
		if (running == true) {
			player.rb2d.velocity = (new Vector2 (h * player.runForce * jumpmove, player.rb2d.velocity.y));
		} else{
			player.rb2d.velocity = (new Vector2 (h * player.moveForce * jumpmove, player.rb2d.velocity.y));
		}
        Vector3 scale = player.rb2d.transform.localScale;
        if (h > 0)
            scale.x = Mathf.Abs(scale.x);
        if (h < 0)
            scale.x = -Mathf.Abs(scale.x);
        player.rb2d.transform.localScale = scale;

	}
	
	public override void onRemove() {
		
	}
}

