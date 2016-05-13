using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject inside;
    public GameObject outside;
    public bool outsideOn;
	
	void Start()
	{
        outsideOn = true;
        outside.SetActive(true);
        inside.SetActive(false);
    }

    public void InOut()
	{
        if (outsideOn) {
            outside.SetActive(false);
            inside.SetActive(true);
        } else {
            outside.SetActive(true);
            inside.SetActive(false);
        }
        outsideOn = !outsideOn;
    }
}
