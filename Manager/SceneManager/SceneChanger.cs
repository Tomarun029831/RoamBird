using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    private static TrackInfoUIController trackInfoUIController = GameObject.FindFirstObjectByType<TrackInfoUIController>();

    public static void ChangeFarwardScene()
    {
        int targetIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (0 < targetIndex && targetIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneAt((uint)targetIndex);
        }
        else if (targetIndex >= SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneAt(1);
        }
    }

    public static void ChangeBackwardScene()
    {
        int targetIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (0 < targetIndex && targetIndex < SceneManager.sceneCountInBuildSettings)
        {
            LoadSceneAt((uint)targetIndex);
        }
    }

    public static void LoadSceneAt(uint index)
    {
        if (index < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene((int)index);
            StageProgressionTracker.Ready(index);
            trackInfoUIController.UpdateTrackInfo(StageProgressionTracker.GetCurrentStageData());
        }
    }

    public static void LoadMainMenu() => LoadSceneAt(0);
}
