using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Resources : MonoBehaviour {

    public static Resources S;

    public float health = 10;
    public float maxHealth = 10;

    public Image healthFill;

    public int energy = 8;    
    public int maxEnergy = 8;

    public List<Image> energyImages;

    public Color energyEmpty;
    public Color energyFull;

    //Text txt;

    void Awake() {
        S = this;
    }

	// Use this for initialization
	void Start () {
        //txt = GetComponentInChildren<Text>();
        UpdateResources();
	}
	
	// Update is called once per frame
	void Update () {

        S.UpdateResources();

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

    void UpdateResources() {
        //txt.text = health.ToString();

        healthFill.fillAmount = health / maxHealth;

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

    public static void Respawn() {
        S.health = S.maxHealth;
        S.energy = S.maxEnergy;
    }

    public static void ChangeEnergy(int amount) {
        if (amount > 0) {
            S.energy = Mathf.Min(S.energy + amount, S.maxEnergy);
        } else {
            S.energy = Mathf.Max(S.energy + amount, 0);
        }
    }

    public static void ChangeHealth(int amount) {
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
