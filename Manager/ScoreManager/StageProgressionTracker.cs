using System;
using System.Collections.Generic;
using System.Diagnostics;

public class TrackingData : System.Collections.Generic.Dictionary<uint, StageData>
{
    public TrackingData() { }
    public TrackingData(IDictionary<uint, StageData> dictionary) : base(dictionary) { }
}

public class StageData
{
    public TimeSpan totalTimer = TimeSpan.Zero;
    public TimeSpan timerPerStage = TimeSpan.MaxValue;
    public uint totalGoalCounter = 0;
    public uint streakGoalCounter = 0;
}

public static class StageProgressionTracker
{
    private static Stopwatch currentTimer = new();
    public enum State
    {
        InTracking,
        InReady,
        InStop,
    }
    public static State state;

    private static uint currentStageBuildIndex;
    public static uint CurrentStageBuildIndex => currentStageBuildIndex;
    private static TrackingData trackingDatas = new();

    public static void Ready(uint stageBuildIndex)
    {
        currentStageBuildIndex = stageBuildIndex;
        AddStage(stageBuildIndex);
        state = State.InReady;
    }

    public static void StartTrack()
    {
        StageData data = GetStageData(currentStageBuildIndex);
        if (state != State.InReady) return;
        currentTimer.Restart();
        state = State.InTracking;
    }

    public static void StopTrack(bool goalAchieved)
    {
        StageData data = GetStageData(currentStageBuildIndex);
        if (data == null || state != State.InTracking) return;
        currentTimer.Stop();

        if (goalAchieved)
        {
            // streak
            data.streakGoalCounter++;
            data.timerPerStage = data.timerPerStage > currentTimer.Elapsed ? currentTimer.Elapsed : data.timerPerStage;
            // total
            data.totalGoalCounter++;
        }
        else
        {
            // reset streak
            data.streakGoalCounter = 0;
        }
        data.totalTimer += currentTimer.Elapsed;

        PushTrackingDatasToDB();
        state = State.InStop;
    }

    private static async void PushTrackingDatasToDB()
    {
        bool result = false;
        while (result == false) result = await TrackerAPIClient.Push(trackingDatas);
    }

    public static TrackingData ExtractStageDatas => trackingDatas;

    public static StageData GetCurrentStageData()
    {
        uint targetIndex = currentStageBuildIndex > 0 ? currentStageBuildIndex : 1;
        return GetStageData(targetIndex);
    }

    public static void SetTrackingData(TrackingData trackindData) => trackingDatas = trackindData;

    private static void AddStage(uint stageBuildIndex)
    {
        if (!trackingDatas.ContainsKey(stageBuildIndex)) trackingDatas[stageBuildIndex] = new StageData();
    }

    private static StageData GetStageData(uint stageBuildIndex)
    {
        StageData data;
        if (!trackingDatas.TryGetValue(stageBuildIndex, out data))
        {
            trackingDatas[stageBuildIndex] = new StageData();
            data = trackingDatas[stageBuildIndex];
        }
        return data;
    }
}
