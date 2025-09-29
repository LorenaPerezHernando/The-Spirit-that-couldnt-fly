using Spirit;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int sceneToLoad;
    public Transform posToLoad;
    public void LoadSceneByIndex(int index)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (index >= 0 && index < sceneCount)
        {
            SceneManager.LoadScene(index);
            InitialPosPlayer();

        }
        else
        {
            Debug.LogWarning("indice no esta en Build");
        }
    }
    public void InitialPosPlayer()
    {
        GameController.Instance.NextInitialPos(posToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
