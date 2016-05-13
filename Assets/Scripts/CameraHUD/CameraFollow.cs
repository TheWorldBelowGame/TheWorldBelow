using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow S;
    
	public GameObject player;
    public Vector3 initOffset;
    public bool falling = false;
	public Camera outside;
	public Camera inside;
	public float speed = 0.1f;

	Vector3 poi;
    Vector3 pos;
    bool outsideOn;
    bool dialogue;

    void Awake()
	{
        S = this;
    }
	
	void Start ()
	{
        transform.position = player.transform.position + initOffset;
        poi = player.transform.position + initOffset;
        outsideOn = true;
        outside.gameObject.SetActive(true);
        inside.gameObject.SetActive(false);
        dialogue = false;
    }
	
    void FixedUpdate ()
	{
        pos = transform.position;

		if (!dialogue) {
			poi = player.transform.position + initOffset;
		}

        pos = Vector3.Lerp(transform.position, poi, speed);
        pos.x = (pos.x < 1.5f ? 1.5f : pos.x);
        transform.position = pos;

    }

    void LateUpdate()
	{
        if (falling) {
            pos = transform.position;
            pos.y = player.transform.position.y;
            transform.position = pos;
        }
    }

    public void InOut()
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

    public void SetPoiPlayer() {
        poi = player.transform.position + initOffset;
        inside.orthographicSize = 5;
        dialogue = false;
    }

    public void SetPoiAverage(Vector3 go1, Vector3 go2) {
        dialogue = true;
        poi.x = (go1.x + go2.x) / 2;
        poi.y = (go1.y + go2.y) / 2 - 0.5f;
        poi.z = -10;
        inside.orthographicSize = 3;
    }
}
