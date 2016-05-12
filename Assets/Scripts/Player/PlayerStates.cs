using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public enum AnimState { Idle, Running, Jumping, Attack, Death, Falling };

namespace PlayerState
{
	public static class Util
	{
		public static int ToInt(this AnimState state)
		{
			switch (state) {
				case AnimState.Idle:
					return 0;
				case AnimState.Running:
					return 1;
				case AnimState.Jumping:
					return 2;
				case AnimState.Attack:
					return 3;
				case AnimState.Death:
					return 4;
				case AnimState.Falling:
					return 5;
				default:
					throw new System.Exception("Invalid AnimState used!");
			}
		}
	}

	public abstract class BasePlayerState : State
	{
		protected Player player;
		protected AnimState animState;

		public BasePlayerState(Player player)
		{
			this.player = player;
		}
	}

	public class Idle : BasePlayerState
	{
		public Idle(Player player) : base(player) {}

		public override void OnStart()
		{
			animState = AnimState.Idle;
		}

		public override void OnUpdate()
		{
			player.anim.SetInteger("State", animState.ToInt());
		}

		public override void OnFinish() {}
	}

	public class Moving : BasePlayerState
	{
		private float moveSpeed;

		public Moving(Player player) : base(player) {}

		public override void OnStart()
		{
			animState = AnimState.Running;
		}

		public override void OnUpdate()
		{
			float moveInputVal = Input.GetAxis(Input_Management.i_Move);

			// Is the player walking or running?
			bool running = Input.GetButton(Input_Management.i_Run) && player.grounded;

			if (player.walled && !player.grounded) {
				// can't run
			} else {
				float moveAmt = moveInputVal * (running ? player.runForce : player.moveForce);
				player.rb2d.velocity.Set(moveAmt, player.rb2d.velocity.y);
			}

			// Control left/right facing
			Vector3 scale = player.rb2d.transform.localScale;
			if (moveInputVal > 0) {
				scale.x = -Mathf.Abs(scale.x);
			} else if (moveInputVal < 0) {
				scale.x = Mathf.Abs(scale.x);
			}
			player.rb2d.transform.localScale = scale;
		}

		public override void OnFinish()
		{

		}
	}

	public class Jumping : Moving
	{
		public Jumping(Player player) : base(player) {}

		public override void OnStart()
		{
			animState = AnimState.Jumping;
		}
	}
}


public class State_Player_Normal_Movement : State
{

    //STATE VARIABLES

	Player player;
    private bool s_jump_input;
    private bool s_running = false;
    private float s_jump_timer;
    private float s_attack_timer;
    private bool s_attacked;


    public State_Player_Normal_Movement(Player player) {
        this.player = player;
    }

    public override void OnStart() {
        s_jump_timer = player.jumpTime;
        s_attack_timer = player.attack_speed;
        s_attacked = false;
        player.sword.SetActive(false);
        s_jump_input = false;
    }

    public override void OnUpdate() {
        if (!player.dead) {
            
            // If the jump button is pressed, jump
            if (Input.GetButtonDown(Input_Management.i_Jump) && player.jumps_left > 0) {
                s_jump_input = true;
                player.jumps_left--;
                player.grounded = false;
            }

            float h = Input.GetAxis(Input_Management.i_Move);

            // Seeing if the player is running or walking
            s_running = Input.GetButton(Input_Management.i_Run) && player.grounded;

            // Jumping
            if (s_jump_input) {
                // Add a vertical force to the player.
                if (s_jump_timer > 0) {
                    player.rb2d.AddForce(new Vector2(0, player.jumpForce), ForceMode2D.Impulse);
                    s_jump_timer -= Time.deltaTime;
                } else {
                    // Make sure the player can't jump again until the jump conditions from Update are satisfied.
                    s_jump_input = false;
                    s_jump_timer = player.jumpTime;
                }
            }



            // Left and right walk movement
            if (!s_attacked) {
                if (player.walled && !player.grounded) {
                    //can't run
                } else if (s_running == true) {
                    player.rb2d.velocity = (new Vector2(h * player.runForce, player.rb2d.velocity.y));
                } else {
                    player.rb2d.velocity = (new Vector2(h * player.moveForce, player.rb2d.velocity.y));
                }
            }
            Vector3 scale = player.rb2d.transform.localScale;
            if (h > 0)
                scale.x = -Mathf.Abs(scale.x);
            if (h < 0)
                scale.x = Mathf.Abs(scale.x);
            player.rb2d.transform.localScale = scale;

            if (Input.GetButtonDown(Input_Management.i_Attack)) {
                s_attacked = true;
            }

            // Attacking
            if (s_attacked) {
                s_attack_timer -= Time.deltaTime;

                // hard coded attack time lol
                if (s_attack_timer <= .333) {
                    player.sword.SetActive(true);
                    if (player.grounded) {
                        player.rb2d.velocity = (new Vector2(0, 0));
                    }
                }
                if (s_attack_timer <= 0) {
                    s_attack_timer = player.attack_speed;
                    player.sword.SetActive(false);
                    s_attacked = false;
                }
            }

            //Doors
            if (player.door != null && Input.GetButtonDown(Input_Management.i_Action)) {
                CameraFollow.S.in_out();
                player.door.GetComponent<Door>().in_out();
                player.spawn = player.transform.position;
            }
        }



		// Animations -----------------------------------------------

		if (player.dead) {
			player.anim.SetInteger ("State", (int)AnimState.death);
			//player.rb2d.velocity = (new Vector2 (0, 0));
		} else if (s_attacked) {
			player.anim.SetInteger ("State", (int)AnimState.attack);
		} else if (player.rb2d.velocity.magnitude > .05f && player.grounded) {
			player.anim.SetInteger ("State", (int)AnimState.running);
		} else if (player.grounded == false) {
			player.anim.SetInteger ("State", (int)AnimState.jumping);
		} else {
			player.anim.SetInteger ("State", (int)AnimState.idle);
		}

    }

    public override void OnFinish() {
    }
}

public class State_Player_Paused : State {
    public State_Player_Paused() {
		
    }
}

public class State_Player_Falling : State {

    
    public State_Player_Falling() {
    }

    public override void OnUpdate() {
        Player.S.anim.SetInteger("State", (int)AnimState.falling);
    }

    public override void OnFinish() {
        
    }
}
