using UnityEngine;
using System.Collections;

public class InteractWithFollower : MonoBehaviour
{
    void Start()
    {
       
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            FollowerMovement.S.player_interrupt = true;
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("Entered Trigger");
        if (coll.gameObject.tag == "Hidden_Platform")
        {
            Debug.Log("Entered Trigger: PLatform");

            FollowerMovement.S.poi_destination = coll.gameObject.transform.position;
            FollowerMovement.S.follower_state = FollowerMovement.State.WaitingForPlayer;

        }
    }
}
