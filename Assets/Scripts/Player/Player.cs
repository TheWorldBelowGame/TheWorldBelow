using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Player : MonoBehaviour
{
	[HideInInspector] public static Player S;
	[HideInInspector] public StateMachine<PlayerState.BasePlayerState> playerSM;

	[HideInInspector] public GameObject door;
	[HideInInspector] public Animator anim;
	[HideInInspector] public Rigidbody2D rb2d;
    [HideInInspector] public bool grounded;
    [HideInInspector] public bool walled;
	[HideInInspector] public bool dead = false;
	[HideInInspector] public bool pause = false;
	[HideInInspector] public Dialogue sign;

	public GameObject sword;
    public Vector3 spawn;

	public void UseDoor()
	{
		CameraFollow.S.InOut();
		door.GetComponent<Door>().InOut();
		spawn = transform.position;
	}
	
	void Awake() 
	{
        S = this;
		playerSM = new StateMachine<PlayerState.BasePlayerState>(new PlayerState.NormalMovement());
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
        grounded = false;
        walled = false;
        door = null;
		sign = null;
        spawn = transform.position;

		InputManagement.init();
	}
	
	void Start()
	{
		sword.SetActive(false);
		playerSM.ChangeState(new PlayerState.NormalMovement());
	}

	void Update()
	{
		if (InputManagement.Start()) {
			SceneManager.LoadScene (0);
		}

        if (dead && !fade.S.fadingOut) {
			Die();
        }
		
		playerSM.Update();
	}

    // Checking if the player is grounded and can jump
    void OnCollisionEnter2D(Collision2D coll) {
        switch (coll.gameObject.tag) {
            case "Ground":
            case "Hidden_Platform":
                grounded = true;
                break;
            case "Wall":
                walled = true;
                break;
		    case "Enemy":
				playerSM.ChangeState(new PlayerState.Dying());
			    dead = true;
                fade.S.fadeOut();
                //die();
                break;
        }
    }

    void OnCollisionExit2D(Collision2D coll) {
		switch (coll.gameObject.tag) {
			case "Wall":
				walled = false;
				break;
		}
    }

    void OnTriggerEnter2D(Collider2D coll) {
		switch (coll.tag) {
			case "Door":
				door = coll.gameObject;
				break;
			case "Dialogue trigger":
				sign = coll.gameObject.GetComponent<Dialogue>();
				break;
		}
    }

    void OnTriggerExit2D(Collider2D coll) {
		switch (coll.tag) {
			case "Door":
				door = null;
				break;
			case "Dialogue trigger":
				sign = null;
				break;
		}
	}

	public void Fall()
	{
		playerSM.ChangeState(new PlayerState.Falling());
	}

    void Die()
	{
        transform.position = spawn;
        Vector3 scale = rb2d.transform.localScale;
        scale.x = Mathf.Abs(scale.x);
        rb2d.transform.localScale = scale;
        Invoke("Undie", 0.5f);
    }
	
    void Undie()
	{
		playerSM.Reset();
        fade.S.fadeIn();
    }
}
