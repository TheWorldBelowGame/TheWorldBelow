using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState {
	idle,
	running
}

public class ElementStandardControl : Element
{
	Player player;
	private bool jump;
	private bool running;
	private float jump_timer;
    private float attack_timer;
    private bool attacked;

	
	public ElementStandardControl(Player player)
	{
		this.player = player;
	}
	
	public override void onActive()
	{
		//player.GetComponent<SimplePlatformController>().enabled = true;
		jump_timer = player.jumpTime;
        attack_timer = player.attack_speed;
        attacked = false;
        player.sword.SetActive(false);
	}
	
	public override void update() {

		//if (Input.GetButtonDown ("Jump") && player.grounded) {
		if (Input.GetButtonDown ("Jump") && player.jumps_left > 0) {
			//Debug.Log("jump");
			jump = true;
			player.jumps_left--;
			//player.grounded = false;
		}

		float h = Input.GetAxis ("Horizontal");
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
			if (jump_timer > 0) {
				player.rb2d.AddForce (new Vector2 (0, player.jumpForce), ForceMode2D.Impulse);
				jump_timer -= Time.deltaTime;
			} else {
				// Make sure the player can't jump again until the jump conditions from Update are satisfied.
				jump = false;
				jump_timer = player.jumpTime;
			}
		}


		
		// Left and right walk movement
		if (running == true) {
			player.rb2d.velocity = (new Vector2 (h * player.runForce * jumpmove, player.rb2d.velocity.y));
		} else {
			player.rb2d.velocity = (new Vector2 (h * player.moveForce * jumpmove, player.rb2d.velocity.y));
			/*if(player.rb2d.velocity.x > 0) {
				player.anim.SetBool("Facing left", false);
			} else if (player.rb2d.velocity.x < 0) {
				player.anim.SetBool("Facing left", true);
			}*/
		}
		Vector3 scale = player.rb2d.transform.localScale;
		if (h > 0)
			scale.x = -Mathf.Abs (scale.x);
		if (h < 0)
			scale.x = Mathf.Abs (scale.x);
		player.rb2d.transform.localScale = scale;

		if (Input.GetButtonDown ("Fire1")) {
			player.sword.SetActive (true);
			attacked = true;
		}

		if (player.rb2d.velocity.magnitude > 0) {
			player.anim.SetInteger ("State", (int)AnimState.running);
		} else {
			player.anim.SetInteger ("State", (int)AnimState.idle);
		}

        if (attacked) {
            attack_timer -= Time.deltaTime;
            if (attack_timer <= 0) {
                attack_timer = player.attack_speed;
                player.sword.SetActive(false);
                attacked = false;
            }
        }

	}
	
	public override void onRemove() {
		
	}
}

