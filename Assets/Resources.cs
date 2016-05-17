using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resources : MonoBehaviour {

    public static Resources S;

    public int health = 3;
    public int energy = 3;

    public int maxHealth = 3;
    public int maxEnergy = 3;

    Text txt;

    void Awake() {
        S = this;
    }

	// Use this for initialization
	void Start () {
        txt = GetComponent<Text>();
        UpdateText();
	}
	
	// Update is called once per frame
	void Update () {
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

    static void UpdateText() {
        S.txt.text = "Health: " + S.health + "\n" + "Energy: " + S.energy;
    }

    public static void Respawn() {
        S.health = S.maxHealth;
        S.energy = S.maxEnergy;
        UpdateText();
    }

    public static void ChangeEnergy(int amount) {
        if (amount > 0) {
            S.energy = Mathf.Min(S.energy + amount, S.maxEnergy);
        } else {
            S.energy = Mathf.Max(S.energy + amount, 0);
        }
        UpdateText();
    }

    public static void ChangeHealth(int amount) {
        if (amount > 0) {
            S.health = Mathf.Min(S.health + amount, S.maxHealth);
        } else {
            S.health = Mathf.Max(S.health + amount, 0);
        }
        UpdateText();
        if (S.health <= 0) {
            Player.S.playerSM.ChangeState(new PlayerState.Dying());
        }
    }
}
