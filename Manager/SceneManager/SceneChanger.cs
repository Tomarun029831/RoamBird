using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    private static TrackInfoUIController trackInfoUIController = GameObject.FindFirstObjectByType<TrackInfoUIController>(); // PERF:

    public static void ChangeFarwardScene()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        bool isSceneIndexPlayableStage = 0 < nextIndex && nextIndex < SceneManager.sceneCountInBuildSettings;
        bool isSceneIndexOutOfRange = nextIndex >= SceneManager.sceneCountInBuildSettings;
        if (isSceneIndexPlayableStage) LoadSceneAt((uint)nextIndex);
        else if (isSceneIndexOutOfRange) LoadSceneAt(1); // load first stage for looping
    }

    public static void ChangeBackwardScene()
    {
        int previousIndex = SceneManager.GetActiveScene().buildIndex - 1;
        bool isSceneIndexPlayableStage = 0 < previousIndex && previousIndex < SceneManager.sceneCountInBuildSettings;
        if (isSceneIndexPlayableStage) LoadSceneAt((uint)previousIndex);
    }

    public static void LoadSceneAt(uint index)
    {
        bool isSceneIndexOutOfRange = index >= SceneManager.sceneCountInBuildSettings;
        if (isSceneIndexOutOfRange) return;
        SceneManager.LoadScene((int)index);
        StageProgressionTracker.Ready(index);
        trackInfoUIController.UpdateTrackInfo(StageProgressionTracker.GetCurrentStageData());
    }

    public static void LoadMainMenu() => LoadSceneAt(0);
}
