using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// Public
	[HideInInspector] public bool falling = false;

	// Visible in Editor
	public GameObject player;
    public Vector3 initOffset;
	public Camera outside;
	public Camera inside;
	public float speed = 0.1f;

	// Private
	static CameraFollow camerasObj;
	Vector3 pointOfInterest;
    Vector3 cameraPos;
    bool outsideOn;
    bool dialogue;
	
	// Static Functions

	// Moves camera to focus on the player
	public static void FocusPlayer()
	{
		camerasObj.SetPoiPlayer();
	}

	// Moves camera to focus in the middle of the player and the given point
	public static void FocusBetweenPlayerAndPoint(Vector3 dialoguePos)
	{
		camerasObj.SetPoiAverage(camerasObj.player.transform.position, dialoguePos);
	}

	// Switch the camera between Inside and Outside mode
	public static void InOut()
	{
		camerasObj.InOutHelper();
	}

	// Private Functions

	void StartDialogueHelper(Vector3 dialoguePos)
	{
		SetPoiAverage(player.transform.position, dialoguePos);
	}
	
	void Start()
	{
		camerasObj = this;
        transform.position = player.transform.position + initOffset;
        pointOfInterest = player.transform.position + initOffset;
        outsideOn = true;
        outside.gameObject.SetActive(true);
        inside.gameObject.SetActive(false);
        dialogue = false;
    }
	
    void FixedUpdate()
	{
        cameraPos = transform.position;

		if (!dialogue) {
			pointOfInterest = player.transform.position + initOffset;
		}

        cameraPos = Vector3.Lerp(transform.position, pointOfInterest, speed);
        cameraPos.x = (cameraPos.x < 1.5f ? 1.5f : cameraPos.x);
        transform.position = cameraPos;

    }

    void LateUpdate()
	{
        if (falling) {
            cameraPos = transform.position;
            cameraPos.y = player.transform.position.y;
            transform.position = cameraPos;
        }
    }

    void InOutHelper()
	{
        if (outsideOn) {
            outside.gameObject.SetActive(false);
            inside.gameObject.SetActive(true);
        } else {
            outside.gameObject.SetActive(true);
            inside.gameObject.SetActive(false);
        }
        outsideOn = !outsideOn;
    }

	void Reset()
	{
		transform.position = player.transform.position + initOffset;
	}

	// Makes the camera focus on the player
    void SetPoiPlayer()
	{
        pointOfInterest = player.transform.position + initOffset;
        inside.orthographicSize = 5;
        dialogue = false;
    }

	// Makes the camera focus on a position in the middle of point1 and point2
    void SetPoiAverage(Vector3 point1, Vector3 point2)
	{
        dialogue = true;
        pointOfInterest.x = (point1.x + point2.x) / 2;
        pointOfInterest.y = (point1.y + point2.y) / 2 - 0.5f;
		pointOfInterest.z = initOffset.z;
        inside.orthographicSize = 3;
    }
}
