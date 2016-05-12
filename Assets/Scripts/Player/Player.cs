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

	void Update()
	{
		if (InputManagement.Start()){
			SceneManager.LoadScene (0);
		}

        if (dead) {
            if (!fade.S.fadingOut)
                die();
        }

		// Doors
		if (door != null && InputManagement.Action()) {
			CameraFollow.S.in_out();
			door.GetComponent<Door>().in_out();
			spawn = transform.position;
		}
		
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
