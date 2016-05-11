using UnityEngine;
using System.Collections;

public class InteractWithFollower : MonoBehaviour {
   


    void Start()
    {
       
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Follower_Movement.S.player_interrupt = true;
        }

    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Entered Trigger");
        if (coll.gameObject.tag == "Hidden_Platform")
        {
            Debug.Log("Entered Trigger: PLatform");

            Follower_Movement.S.poi_destination = coll.gameObject.transform.position;
            Follower_Movement.S.follower_state = Follower_Movement.State.WaitingForPlayer;

        }
    }
}
