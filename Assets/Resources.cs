using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Resources : MonoBehaviour {

    //Singleton
    public static Resources S;

    //Public
    public float health = 10;
    public float maxHealth = 10;

    public Image healthFill;

    public int energy = 8;    
    public int maxEnergy = 8;

    public List<Image> energyImages;

    public Color energyEmpty;
    public Color energyFull;

    void Awake() {
        S = this;
    }

	// Use this for initialization
	void Start () {
        UpdateResources();
	}
	
	// Update is called once per frame
	void Update () {

        S.UpdateResources();

        //This is for testing
        if (Input.GetKeyDown(KeyCode.O)) {
            ChangeHealth(1);
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            ChangeHealth(-1);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            ChangeEnergy(1);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            ChangeEnergy(-1);
        }
        if (Input.GetKeyDown(KeyCode.P)) {
            Respawn();
        }
    }

    // Update Gui with current resources
    // Called every frame
    void UpdateResources() {

        //Update Health
        healthFill.fillAmount = health / maxHealth;

        //Update Energy
        for (int i = 0; i < energyImages.Count; i++) {
            if (maxEnergy <= i) {
                energyImages[i].color = Color.clear;
            } else if (energy > i) {
                energyImages[i].color = energyFull;
            } else {
                energyImages[i].color = energyEmpty;
            }
        }
    }

    // Resets Resources
    // Possible to change to reset to non max values?
    public static void Respawn() {
        S.health = S.maxHealth;
        S.energy = S.maxEnergy;
    }

    // Change Energy by amount
    public static void ChangeEnergy(int amount) {
        if (amount > 0) {
            S.energy = Mathf.Min(S.energy + amount, S.maxEnergy);
        } else {
            S.energy = Mathf.Max(S.energy + amount, 0);
        }
    }

    //Changes Health by amount
    public static void ChangeHealth(float amount) {
        if (amount > 0) {
            S.health = Mathf.Min(S.health + amount, S.maxHealth);
        } else {
            S.health = Mathf.Max(S.health + amount, 0);
        }
        if (S.health <= 0) {
            Player.S.playerSM.ChangeState(new PlayerState.Dying());
        }
    }
}
