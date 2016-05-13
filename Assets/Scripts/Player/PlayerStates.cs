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
	}

	public class NormalMovement : BasePlayerState
	{
		const float kWalkForce = 4.0f;
		const float kRunForce = 6.5f;
		const float kJumpForce = 11f;
		const float kHighJumpForce = 13f;

		Rigidbody2D rb;
		bool canAttack;

		public override void Start()
		{
			canAttack = true;
			rb = Player.S.rb2d;
			SetAnim(AnimState.Idle);
		}

		public override void CheckState()
		{
			if (InputManagement.Attack() && canAttack) {
				Transition(new Attacking());
			} else if (InputManagement.Speak() && Player.S.sign != null) {
				Player.S.sign.StartReading();
				Transition(new Talking());
			}
}

		public override void Update()
		{
			float moveInput = InputManagement.Move();
			bool jump = InputManagement.Jump();
			bool run = InputManagement.Run();

			float jumpForce = kJumpForce;
			float maxSpeed = kWalkForce;

			if (run) {
				jumpForce = kHighJumpForce;
				maxSpeed = kRunForce;
			}

			// Jumping
			if (jump && Player.S.grounded) {
				canAttack = false;
				Player.S.grounded = false;
				Player.S.rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			} else if (Player.S.grounded) {
				canAttack = true;
			}
			
			// Movement
			if (moveInput == 0f && Player.S.grounded) {
				SetAnim(AnimState.Idle);
			} else {
				SetAnim(AnimState.Running);
				if (!Player.S.grounded && Player.S.walled) {
					// cant move
				} else {
					rb.velocity = new Vector2(moveInput * maxSpeed, rb.velocity.y);
				}
			}

			// Control left/right facing
			Vector3 scale = rb.transform.localScale;
			if (moveInput > 0) {
				scale.x = -Mathf.Abs(scale.x);
			} else if (moveInput < 0) {
				scale.x = Mathf.Abs(scale.x);
			}
			rb.transform.localScale = scale;

			// Doors
			Debug.Log(Player.S.door.ToString());
			if (InputManagement.Action() && Player.S.door != null) {
				Player.S.UseDoor();
			}
		}
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
				Transition(new NormalMovement());
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
				Transition(new NormalMovement());
			}
		}

		public override void Update() {}
	}
}