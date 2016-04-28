using UnityEngine;
using System.Collections;

public class LightAmmo : MonoBehaviour {


    private float delay = LightPlatformEnabler.S.bullet_lifetime;
    private GameObject light_bullet_1;
    private GameObject light_bullet_2;


    void Update()
    {
        //The Light Projectile Should be Destroyed after *delay*  seconds by default 
        Destroy(this.gameObject, delay);
        //Detonate the projectile into 2 new projectiles in the opposite direction
        if (Input.GetKeyDown(KeyCode.O))
        {
            light_bullet_1 = Instantiate(this.gameObject, this.gameObject.transform.position + new Vector3(0, 1,0) , this.gameObject.transform.rotation) as GameObject;
            light_bullet_2 = Instantiate(this.gameObject, this.gameObject.transform.position + new Vector3(0,-1,0) , this.gameObject.transform.rotation) as GameObject;
            
            light_bullet_1.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 100));
            light_bullet_2.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -100));
            Destroy(this.gameObject);
        }
    }


    void OnTriggerEnter2D(Collider2D coll)
    {
        //if The Projectiles hit the Hidden Platforms then reveal them
        if (coll.gameObject.tag == "Hidden_Platform")
        {
            Debug.Log("The LightProjectileHit!");
            coll.gameObject.GetComponent<Hidden_Platform>().togglePlatform();
            //coll.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            LightPlatformEnabler.S.light_ammo++;
            Destroy(this.gameObject);
        }
    }



}
