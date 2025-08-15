using UnityEngine;
using System;

public static class SceneInitializer
{
    private static TrackInfoUIController trackInfoUIController = GameObject.FindFirstObjectByType<TrackInfoUIController>();
    public static void InitializeScene()
    {
        Entity[] entities = SceneScanner.ScanAllEntities();
        CameraHandler cameraHandler = SceneScanner.ScanCameraHandler();
        Array.ForEach(entities, e => e.Init());
        cameraHandler.Init();

        if (StageProgressionTracker.state == StageProgressionTracker.State.InTracking) StageProgressionTracker.StopTrack(goalAchieved: false);
        StageProgressionTracker.Ready(StageProgressionTracker.CurrentStageBuildIndex);
        trackInfoUIController.UpdateTrackInfo(StageProgressionTracker.GetCurrentStageData());
    }
}
