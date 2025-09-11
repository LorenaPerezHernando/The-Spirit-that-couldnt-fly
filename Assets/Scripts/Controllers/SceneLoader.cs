using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int _sceneToLoad;
    public void LoadSceneByIndex(int index)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (index >= 0 && index < sceneCount)
        {
            SceneManager.LoadScene(index);
        }
        else
        {
            Debug.LogWarning("indice no esta en Build");
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
