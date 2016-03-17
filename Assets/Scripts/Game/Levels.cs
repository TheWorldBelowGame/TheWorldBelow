using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour {

    public void gotoLevel(string sname) {
        SceneManager.LoadScene(sname);
    }
}
