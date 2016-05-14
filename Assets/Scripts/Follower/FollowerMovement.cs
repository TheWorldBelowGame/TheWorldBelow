using UnityEngine;
using System.Collections;

public class FollowerMovement : MonoBehaviour
{
    //Singleton 
    public static FollowerMovement S;
    
    //Public Variables
    public GameObject player;
    public float outer_radius;
    public float speed;


    // Private variables
    [HideInInspector]
    public enum State {Idle, Busy, WaitingForPlayer }
    public State follower_state;
    public bool player_interrupt;
    private Vector2 destination;
    public Vector2 poi_destination;
	
	void Awake()
    {
        S = this;
	}

    void Start()
    {
        poi_destination = transform.position;
    }
	
	void Update () {
        // Follower State:Busy

        if (follower_state == State.Busy)
        {
           // Debug.Log("Follower is now Busy");
            //Check if we are already at our desired location
            if (Vector2.Distance(transform.position, destination) < 0.5)
            {
                follower_state = State.Idle;
            }
            else
            {
                float tempx = transform.position.x;
                float tempy = transform.position.y;
                transform.position = Vector2.MoveTowards(new Vector2(tempx, tempy), destination, speed * Time.deltaTime);

            }
        }

        // Follower State:Idle
        else if (follower_state == State.Idle)
        {
            if (player_interrupt)
            {
                Debug.Log("Player Interrupted");
                //Do things the player would ask you to do before going back to moving around
                player_interrupt = false;
            }

            //Debug.Log("Follower is now Idle");
            destination = GetPosition();
            // Debug.Log("Going to: " + destination);
            follower_state = State.Busy;
        }

        // Follower State: Waiting For Player
        else if (follower_state == State.WaitingForPlayer)
        {
            //Check if we are already at our desired location
            if (Vector2.Distance(transform.position, poi_destination) < 0.5)
            {
                Debug.Log("Waiting for orders");
                if (player_interrupt)
                {
                    Debug.Log("Player Interrupted");
                    //Do things the player would ask you to do before going back to moving around
                    player_interrupt = false;
                    follower_state = State.Idle;
                }
            }
            else
            {
                float tempx = transform.position.x;
                float tempy = transform.position.y;
                transform.position = Vector2.MoveTowards(new Vector2(tempx, tempy), poi_destination, speed * Time.deltaTime);

            }
        }
    }









    private Vector2 GetPosition()
    {
        float player_x = player.gameObject.transform.position.x;
        float player_y = player.gameObject.transform.position.y;

        float radius_x = Random.Range(player_x - outer_radius, player_x + outer_radius);
        float radius_y = Random.Range(player_y, player_y + outer_radius);

        //Debug.Log("new destination: " + new Vector2(radius_x, radius_y));
        return new Vector2(radius_x, radius_y);
       
    }


}
