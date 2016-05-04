using UnityEngine;
using System.Collections;

public class InteractWithFollower : MonoBehaviour {

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Follower_Movement.S.player_interrupt = true;
            Follower_Movement.S.follower_state = Follower_Movement.State.Idle;
        }

    }
}
