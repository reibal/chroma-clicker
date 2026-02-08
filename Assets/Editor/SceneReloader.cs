using UnityEditor;
using UnityEngine.SceneManagement;

// DISCLAIMER: THIS SCRIPT IS INTENDED FOR DEBUG PURPOSES ONLY

public class SceneReloader
{
    // You can press "Ctrl + R" to run RestartScene()
    [MenuItem("Helpers/Restart Scene ^R")]
    private static void RestartScene()
    {
        var currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
