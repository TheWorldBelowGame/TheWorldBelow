using UnityEngine;
using System.Collections;

// Container namespace for everything related to Player States
namespace PlayerState
{
	public enum AnimState { Idle, Running, Jumping, Attack, Death, Falling };

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
		// Only here because most states don't need to do anything on Finish
		// This reduces the compiler errors for not implementing the Finish function lol
		public override void Finish() {}

		protected void SetAnim(AnimState state)
		{
			Player.S.anim.SetInteger("State", state.ToInt());
		}
	}

	// Player can walk, run and jump as normal.
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
			} else if (InputManagement.Speak() && Player.S.dialogue != null) {
				Player.S.dialogue.StartReading();
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
			if (InputManagement.Action() && Player.S.door != null) {
				Player.S.UseDoor();
			}
		}
	}

	// Player is interacting with dialogue, and cannot take any action until the dialogue is finished.
	public class Talking : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Idle);
		}

		public override void CheckState() {}

		public override void Update()
		{
			if (InputManagement.Speak()) {
				Player.S.dialogue.Advance();
			}
		}
	}

	// Player is attacking, and cannot take any other action until attack is finished
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

	// Used when the player falls off of the island at the beginning of the game
	// TODO: Could probably combine this with the Talking state, since the only difference is the animation
	public class Falling : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Falling);
		}

		public override void CheckState() {}

		public override void Update()
		{
			if (InputManagement.Speak()) {
				Player.S.dialogue.Advance();
			}
		}
	}

	// Activated when the player dies to an enemy, takes care of death and respawning
	public class Dying : BasePlayerState
	{
		// Amount of time to keep the screen dark before respawning
		const float deadTime = 0.2f;
		GameObject deathHelper;

		void Respawn()
		{
			Player.S.transform.position = Player.S.spawn;
			Vector3 scale = Player.S.rb2d.transform.localScale;
			scale.x = Mathf.Abs(scale.x);
			Player.S.rb2d.transform.localScale = scale;
			SetAnim(AnimState.Idle);
			CameraFollow.S.Reset();
            Resources.Respawn();
		}

		IEnumerator Die()
		{
			Fade.FadeOut();
			// Wait for fade out to finish before continuing
			yield return new WaitForSeconds(Fade.defaultFadeTime);
			Respawn();

			yield return new WaitForSeconds(deadTime);
			Fade.FadeIn();
			Transition(new NormalMovement());
		}

		public override void Start()
		{
			SetAnim(AnimState.Death);

			// Have to start coroutine using a monobehaviour
			deathHelper = new GameObject("DeathHelper");
			MonoBehaviour deathHelperScript = deathHelper.AddComponent<MonoBehaviour>();
			deathHelperScript.StartCoroutine(Die());
		}

		public override void CheckState() {}

		public override void Update() {}

		public override void Finish()
		{
			GameObject.Destroy(deathHelper);
		}
	}
}