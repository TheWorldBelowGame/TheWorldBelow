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
			} else if (InputManagement.Speak() && Player.S.sign != null) {
				Player.S.sign.StartReading();
				Transition(new Talking());
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

	public class Talking : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Idle);
		}

		public override void CheckState() {}

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
			} else if (InputManagement.Speak() && Player.S.sign != null) {
				Player.S.sign.StartReading();
				Transition(new Talking());
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
			} else if (InputManagement.Speak() && Player.S.sign != null) {
				Player.S.sign.StartReading();
				Transition(new Talking());
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
			// TODO: Make it so you can still move in the direction that is not walled
			if (!Player.S.walled) {
				MovePlayer(true, Walking.kWalkForce);
			}
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