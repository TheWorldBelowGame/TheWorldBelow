using UnityEngine;
using System.Collections;

public class LightPlatformEnabler : MonoBehaviour {

    //Public Variables
    public static LightPlatformEnabler S;
    public float bullet_lifetime = 1f;
    public GameObject LightProjectile;
    public int light_ammo = 2;


    //Private helper Variables
    private GameObject light_bullet;

    void Awake()
    {
        S = this;
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.P) && light_ammo>0)
        {
            light_ammo--;
            light_bullet = Instantiate(LightProjectile,transform.position, LightProjectile.transform.rotation) as GameObject;
            light_bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 0));
        }
	}



}
