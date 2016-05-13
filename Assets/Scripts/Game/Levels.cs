using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void GotoLevel(string sceneName)
	{
        SceneManager.LoadScene(sceneName);
    }
}
