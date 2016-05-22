using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
	// DEFINITIONS
	public enum AnimState { Idle, Running, Jumping, Attack, Death, Falling };

	// SINGLETON
	[HideInInspector] public static Player S;
	[HideInInspector] StateMachine<BasePlayerState> playerSM;

	// PUBLIC
	[HideInInspector] public GameObject door;
	[HideInInspector] public Animator anim;
	[HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public bool grounded;

	// VISIBLE IN EDITOR
	public float WALK_FORCE = 4.0f;
	public float RUN_FORCE = 6.5f;
	public float JUMP_FORCE = 11f;
	public float HIGH_JUMP_FORCE = 13f;
	public float ATTACK_SPEED = 0.25f;
	public GameObject sword;
    public Vector3 spawn;

	// PRIVATE
	Interactable interactableObj;

	// PLAYER STATES

	abstract class BasePlayerState : State
	{
		// Only here because most states don't need to do anything on Finish
		// This reduces the compiler errors for not implementing the Finish function lol
		public override void Finish() { }

		protected void SetAnim(AnimState state)
		{
			S.anim.SetInteger("State", state.ToInt());
		}
	}

	// Player can walk, run and jump.
	class NormalMovement : BasePlayerState
	{
		Rigidbody2D rb;
		bool canAttack;

		void UseDoor()
		{
			// Workaround because OnTriggerExit doesn't fire when we use doors?
			S.interactableObj = null;

			CameraFollow.InOut();
			S.door.GetComponent<Door>().InOut();
			S.spawn = S.transform.position;
		}

		public override void Start()
		{
			canAttack = true;
			rb = S.rb2d;
			SetAnim(AnimState.Idle);
		}

		public override void Update()
		{
			if (InputManagement.Attack() && canAttack) {
				Transition(new Attacking());
			} else if (InputManagement.Speak() && S.interactableObj != null) {
				S.interactableObj.Interact();
				Transition(new Talking());
			}

			float moveInput = InputManagement.Move();
			bool jump = InputManagement.Jump();
			bool run = InputManagement.Run();

			float jumpForce = S.JUMP_FORCE;
			float maxSpeed = S.WALK_FORCE;

			if (run) {
				jumpForce = S.HIGH_JUMP_FORCE;
				maxSpeed = S.RUN_FORCE;
			}

			// Jumping
			if (jump && S.grounded) {
				canAttack = false;
				S.grounded = false;
				S.rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
			} else if (S.grounded) {
				canAttack = true;
			}

			// Movement
			if (moveInput == 0f && S.grounded) {
				SetAnim(AnimState.Idle);
			} else {
				SetAnim(AnimState.Running);
				rb.velocity = new Vector2(moveInput * maxSpeed, rb.velocity.y);
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
			if (InputManagement.Action() && S.door != null) {
				UseDoor();
			}
		}
	}

	// Player is interacting with dialogue, and cannot take any action until the dialogue is finished.
	class Talking : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Idle);
		}

		public override void Update()
		{
			if (InputManagement.Speak()) {
				bool moreDialogue = DialogueEngine.Advance();
				if (!moreDialogue) {
					Transition(new NormalMovement());
				}
			}
		}
	}

	// Player is attacking, and cannot take any other action until attack is finished
	class Attacking : BasePlayerState
	{
		float attackTimer;
		
		public override void Start()
		{
			SetAnim(AnimState.Attack);
			attackTimer = S.ATTACK_SPEED;
			S.sword.SetActive(true);
			S.rb2d.velocity = (new Vector2(0, S.rb2d.velocity.y));
		}

		public override void Update()
		{
			attackTimer -= Time.deltaTime;

			if (attackTimer <= 0) {
				S.sword.SetActive(false);
				Transition(new NormalMovement());
			}
		}
	}

	// Used when the player falls off of the island at the beginning of the game
	// TODO: Could probably combine this with the Talking state, since the only difference is the animation
	class Falling : BasePlayerState
	{
		public override void Start()
		{
			SetAnim(AnimState.Falling);
		}

		public override void Update()
		{
			if (InputManagement.Speak()) {
				DialogueEngine.Advance();
			}
		}
	}

	// Activated when the player dies to an enemy, takes care of death and respawning
	class Dying : BasePlayerState
	{
		// Amount of time to keep the screen dark before respawning
		const float deadTime = 0.2f;
		// Temporary GameObject to help us run Die() as a coroutine
		GameObject deathHelper;

		void Respawn()
		{
			S.transform.position = S.spawn;
			Vector3 scale = S.rb2d.transform.localScale;
			scale.x = Mathf.Abs(scale.x);
			S.rb2d.transform.localScale = scale;
			SetAnim(AnimState.Idle);
			CameraFollow.FocusPlayer();
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

		public override void Update() { }

		public override void Finish()
		{
			Destroy(deathHelper);
		}
	}

	// FUNCTIONS

	// Kills the player. Public so that other scripts (specifically Resource Management) can cause player to die
	public static void Kill()
	{
		S.playerSM.ChangeState(new Dying());
	}

	void Awake() 
	{
        S = this;
	}
	
	void Start()
	{
		playerSM = new StateMachine<BasePlayerState>(new NormalMovement());
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		grounded = false;
		spawn = transform.position;

		sword.SetActive(false);
		playerSM.ChangeState(new NormalMovement());
	}

	void Update()
	{
		if (InputManagement.Start()) {
			SceneManager.LoadScene(0);
		}
		
		playerSM.Update();
	}
	
    void OnCollisionEnter2D(Collision2D coll)
	{
        switch (coll.gameObject.tag) {
            case "Ground":
            case "Hidden_Platform":
                grounded = true;
                break;
		    case "Enemy":
                Resources.ChangeHealth(-1);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
	{
		Interactable targetInteractable = coll.gameObject.GetComponent<Interactable>();
		if (interactableObj == null && targetInteractable != null) {
			interactableObj = targetInteractable;
		}

		switch (coll.tag) {
			case "Door":
				door = coll.gameObject;
				break;
			case "FallingTrigger":
				playerSM.ChangeState(new Falling());
				break;
			case "FallingDialogue":
				rb2d.isKinematic = true;
				DialogueEngine.Begin("falling");
				break;
            case "HealthPickup":
                Resources.ChangeHealth(1);
                break;
            case "EnergyPickup":
                Resources.ChangeEnergy(1);
                break;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
	{
		if (interactableObj != null && interactableObj == coll.gameObject.GetComponent<Interactable>()) {
			interactableObj = null;
		}

		switch (coll.tag) {
			case "Door":
				door = null;
				break;
		}
	}
}
