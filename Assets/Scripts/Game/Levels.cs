using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

// Utility class for changing levels.
public class Levels
{
    public void GotoLevel(string sceneName)
	{
        SceneManager.LoadScene(sceneName);
    }
}
