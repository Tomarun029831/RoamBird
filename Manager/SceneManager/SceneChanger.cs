using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static void ChangeFarwardScene()
    {
        int targetIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (0 < targetIndex && targetIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(targetIndex);
            return;
        }
        if(targetIndex >= SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneAt(1);
            return;
        }
    }

    public static void ChangeBackwardScene()
    {
        int targetIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (0 < targetIndex && targetIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(targetIndex);
        }
    }

    public static void LoadSceneAt(int _index)
    {
        if (0 < _index && _index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(_index);
        }
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
