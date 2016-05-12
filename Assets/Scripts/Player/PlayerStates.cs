using UnityEngine;
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
		public override void Finish() {}

		protected void SetAnim(AnimState state)
		{
			Player.S.anim.SetInteger("State", state.ToInt());
		}

		// Move the player using a percentage of the given max speed based on input
		protected void MovePlayer(bool canMove, float maxSpeed)
		{
			float moveInputVal = InputManagement.Move();
			Rigidbody2D rb = Player.S.rb2d;

			if (canMove) {
				//rb.velocity.Set(moveInputVal * maxSpeed, rb.velocity.y);
				rb.velocity = new Vector2(moveInputVal * maxSpeed, rb.velocity.y);
			} else {
				//rb.velocity.Set(0, rb.velocity.y);
			}

			// Control left/right facing
			Vector3 scale = rb.transform.localScale;
			if (moveInputVal > 0) {
				scale.x = -Mathf.Abs(scale.x);
			} else if (moveInputVal < 0) {
				scale.x = Mathf.Abs(scale.x);
			}
			rb.transform.localScale = scale;
		}
	}

	public class Idle : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Idle);
		}

		public override void CheckState()
		{
			if (InputManagement.Jump()) {
				Transition(new Jumping());
			} else if (InputManagement.Attack()) {
				Transition(new Attacking());
			} else if (InputManagement.Move() != 0) {
				if (InputManagement.Run()) {
					Transition(new Running());
				} else {
					Transition(new Walking());
				}
			}
		}

		public override void Update() {}
	}

	public class Walking : BasePlayerState
	{
		public const float kWalkForce = 4.0f;

		public override void Start()
		{
			SetAnim(AnimState.Running);
		}

		public override void CheckState()
		{
			if (InputManagement.Jump()) {
				Transition(new Jumping());
			} else if (InputManagement.Attack()) {
				Transition(new Attacking());
			} else if (InputManagement.Move() == 0f) {
				Transition(new Idle());
			} else if (InputManagement.Run()) {
				Transition(new Running());
			}
		}

		public override void Update()
		{
			MovePlayer(true, kWalkForce);
		}
	}

	public class Running : BasePlayerState
	{
		const float kRunForce = 6.5f;

		public override void Start()
		{
			SetAnim(AnimState.Running);
		}

		public override void CheckState()
		{
			if (InputManagement.Jump()) {
				Transition(new Jumping());
			} else if (InputManagement.Attack()) {
				Transition(new Attacking());
			} else if (InputManagement.Move() == 0f) {
				Transition(new Idle());
			} else if (!InputManagement.Run()) {
				Transition(new Walking());
			}
		}

		public override void Update()
		{
			MovePlayer(true, kRunForce);
		}
	}

	public class Jumping : BasePlayerState
	{
		const float kJumpForce = 11f;
		const float kHighJumpForce = 13f;

		float jumpForce;

		public override void Start()
		{
			SetAnim(AnimState.Jumping);

			jumpForce = kJumpForce;
			if (InputManagement.Run()) {
				jumpForce = kHighJumpForce;
			}

			Player.S.grounded = false;
			Player.S.jumps_left--;
			Player.S.rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}

		public override void CheckState()
		{
			if (Player.S.grounded) {
				Transition(new Idle());
				/*
				if (InputManagement.Jump()) {
					Transition(new Jumping);
				} else if (InputManagement.Move() != 0f) {
					Transition(new Idle());
				} else {
					if (InputManagement.Run()) {
						Transition(new Running());
					} else {
						Transition(new Walking());
					}
				}
				*/
			}
		}

		public override void Update()
		{
			// Player can only move at walking speed while jumping
			MovePlayer(true, Walking.kWalkForce);
		}
	}

	public class Attacking : BasePlayerState
	{
		const float kAttackSpeed = 0.25f;
		
		float attackTimer;

		public override void Start()
		{
			SetAnim(AnimState.Attack);
			attackTimer = kAttackSpeed;
			Player.S.sword.SetActive(true);
			Player.S.rb2d.velocity = (new Vector2(0, Player.S.rb2d.velocity.y));
		}

		// Can't take any actions until attack is over
		public override void CheckState()
		{
			if (attackTimer <= 0) {
				Player.S.sword.SetActive(false);
				Transition(new Idle());
			}
		}

		public override void Update()
		{
			attackTimer -= Time.deltaTime;
		}
	}

	public class Falling : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Falling);
		}

		public override void CheckState() {}

		public override void Update() {}
	}

	public class Dying : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Death);
		}

		public override void CheckState()
		{
			if (fade.S.fadingIn == true) {
				Transition(new Idle());
			}
		}

		public override void Update() {}
	}
}


/*
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
            if (Input.GetButtonDown(InputManagement.i_Jump) && player.jumps_left > 0) {
                s_jump_input = true;
                player.jumps_left--;
                player.grounded = false;
            }

            float h = Input.GetAxis(InputManagement.i_Move);

            // Seeing if the player is running or walking
            s_running = Input.GetButton(InputManagement.i_Run) && player.grounded;

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

            if (Input.GetButtonDown(InputManagement.i_Attack)) {
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
            if (player.door != null && Input.GetButtonDown(InputManagement.i_Action)) {
                CameraFollow.S.in_out();
                player.door.GetComponent<Door>().in_out();
                player.spawn = player.transform.position;
            }
        }



		// Animations -----------------------------------------------

		if (player.dead) {
			player.anim.SetInteger ("State", (int)AnimState.Death);
			//player.rb2d.velocity = (new Vector2 (0, 0));
		} else if (s_attacked) {
			player.anim.SetInteger ("State", (int)AnimState.Attack);
		} else if (player.rb2d.velocity.magnitude > .05f && player.grounded) {
			player.anim.SetInteger ("State", (int)AnimState.Running);
		} else if (player.grounded == false) {
			player.anim.SetInteger ("State", (int)AnimState.Jumping);
		} else {
			player.anim.SetInteger ("State", (int)AnimState.Idle);
		}

    }

    public override void OnFinish() {
    }
}
*/