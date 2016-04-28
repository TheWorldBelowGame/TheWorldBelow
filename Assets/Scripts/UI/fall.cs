using UnityEngine;
using System.Collections;

public class fall : MonoBehaviour {

    public scroll back1;
    public scroll back2;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Player.S.player_state_machine.ChangeState(new State_Player_Falling());
            back1.begin();
            back2.begin();
        }
    }
}
