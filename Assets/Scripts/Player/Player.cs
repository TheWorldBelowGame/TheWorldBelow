using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
	[HideInInspector] public static Player S;
	public StateMachine<PlayerState.BasePlayerState> playerSM;

	[HideInInspector] public GameObject door;
	[HideInInspector] public Animator anim;
	[HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool walled;
	[HideInInspector] public bool dead = false;
	[HideInInspector] public bool pause = false;
	[HideInInspector] public int jumps_left;
	//public float moveForce = 365f;
	//public float runForce = 500f;
	//public float jumpForce = 1000f;
	//public float jumpTime = 1f;
	//public bool doubleJump = true;
    //public float attack_speed = 0.25f;

    public GameObject sword;
    public Vector3 spawn;
	
	void Awake() 
	{
        S = this;
		playerSM = new StateMachine<PlayerState.BasePlayerState>(new PlayerState.Idle());
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        jumps_left = 0;
        grounded = false;
        walled = false;
        door = null;
        spawn = transform.position;

		InputManagement.init();
	}
	
	void Start()
	{
		playerSM.ChangeState(new PlayerState.Idle());
	}

	/*
	void StateTransition()
	{
		Type state = playerSM.GetCurrentState();

		if (state == typeof(PlayerState.Jumping) && grounded) {
			playerSM.Reset();
			state = playerSM.GetCurrentState();
		}

		PlayerState.BasePlayerState newState = null;
		Debug.Log(state.ToString());

		bool inputJump = Input.GetButtonDown(InputManagement.i_Jump);
		bool inputRun = Input.GetButton(InputManagement.i_Run);
		bool inputAttack = Input.GetButton(InputManagement.i_Attack);
		float inputMove = Input.GetAxis(InputManagement.i_Move);

		if (inputAttack && state != typeof(PlayerState.Jumping)) {
			newState = new PlayerState.Attacking(anim, rb2d, sword);
		} else if (inputJump && jumps_left > 0 && grounded && state != typeof(PlayerState.Attacking)) {
			grounded = false;
			jumps_left--;
			newState = new PlayerState.Jumping(anim, rb2d, inputRun);
		} else if (grounded && state != typeof(PlayerState.Attacking)) {
			if (inputMove != 0) {
				if (inputRun) {
					newState = new PlayerState.Running(anim, rb2d);
				} else {
					newState = new PlayerState.Walking(anim, rb2d);
				}
			} else {
				newState = new PlayerState.Idle(anim);
			}
		}

		if (newState != null) {
			playerSM.ChangeState(newState);
		}
	}
	*/

	void Update()
	{
		if (InputManagement.Start()){
			SceneManager.LoadScene (0);
		}

        if (dead) {
            if (!fade.S.fadingOut)
                die();
        }

		//Doors
		if (door != null && InputManagement.Action()) {
			CameraFollow.S.in_out();
			door.GetComponent<Door>().in_out();
			spawn = transform.position;
		}

		//StateTransition();
		playerSM.Update();
	}

    // Checking if the player is grounded and can jump
    void OnCollisionEnter2D(Collision2D coll) {
        switch (coll.gameObject.tag) {
            case "Ground":
            case "Hidden_Platform":
                grounded = true;
                jumps_left = 1;
                break;
            case "Wall":
                walled = true;
                break;
		    case "Enemy":
				playerSM.ChangeState(new PlayerState.Dying());
			    dead = true;
                fade.S.fadingOut = true;
                //die();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
        if (coll.gameObject.tag == "Wall") {
            walled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == ("Door")) {
            door = trigger.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D trigger) {
        if (trigger.gameObject.tag == ("Door")) {
            door = null;
        }
    }

	// For now just make the player idle when they are paused
	public void Pause()
	{
		playerSM.ChangeState(new PlayerState.Idle());
	}

	public void Fall()
	{
		playerSM.ChangeState(new PlayerState.Falling());
	}

    void die() {
        transform.position = spawn;
        Vector3 scale = rb2d.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        rb2d.transform.localScale = scale;
        Invoke("undie", 0.5f);
    }

    void undie() {
		playerSM.Reset();
		fade.S.fadingIn = true;
    }
}
