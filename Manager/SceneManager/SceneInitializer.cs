using UnityEngine;

public static class SceneInitializer
{
    private static TrackInfoUIController trackInfoUIController = GameObject.FindFirstObjectByType<TrackInfoUIController>();
    public static void InitializeScene()
    {
        Entity[] entities = SceneScanner.ScanAllEntities();
        CameraHandler cameraHandler = SceneScanner.ScanCameraHandler();
        foreach (Entity entity in entities)
        {
            entity.Init();
        }
        cameraHandler.Init();

        if (StageProgressionTracker.state == StageProgressionTracker.State.InTracking) { StageProgressionTracker.StopTrack(false); }
        StageProgressionTracker.Ready(StageProgressionTracker.CurrentStageBuildIndex);
        trackInfoUIController.UpdateTrackInfo(StageProgressionTracker.GetCurrentStageData());
    }
}
