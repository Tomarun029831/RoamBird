using System.Collections.Generic;
using System;
using System.Diagnostics;

public class StageData
{
    public TimeSpan totalTimer = TimeSpan.Zero;
    public TimeSpan timerPerStage = TimeSpan.MaxValue;
    public Stopwatch currentTimer = new();
    public uint totalGoalCounter = 0;
    public uint streakGoalCounter = 0;
}

public static class StageProgressionTracker
{
    public enum State
    {
        InTracking,
        InReady,
        InStop,
    }
    public static State state;

    private static uint currentStageBuildIndex;
    public static uint CurrentStageBuildIndex => currentStageBuildIndex;
    private static Dictionary<uint, StageData> stages = new();

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
        data.currentTimer.Restart();
        state = State.InTracking;
    }

    public static void StopTrack(bool goalAchieved)
    {
        StageData data = GetStageData(currentStageBuildIndex);
        if (data == null || state != State.InTracking) return;
        data.currentTimer.Stop();

        if (goalAchieved)
        {
            // === Streak ===
            data.streakGoalCounter++;
            data.timerPerStage = data.timerPerStage > data.currentTimer.Elapsed ? data.currentTimer.Elapsed : data.timerPerStage;
            // === Total ===
            data.totalGoalCounter++;
        }
        else
        {
            data.streakGoalCounter = 0;
        }
        data.totalTimer += data.currentTimer.Elapsed;

        state = State.InStop;
    }

    public static Dictionary<uint, StageData> ExtractStageDatas => stages;

    public static StageData GetCurrentStageData()
    {
        uint targetIndex = currentStageBuildIndex > 0 ? currentStageBuildIndex : 1;
        return GetStageData(targetIndex);
    }

    private static void AddStage(uint stageBuildIndex)
    {
        if (!stages.ContainsKey(stageBuildIndex))
        {
            stages[stageBuildIndex] = new StageData();
        }
    }

    private static StageData GetStageData(uint stageBuildIndex)
    {
        StageData data;
        if (!stages.TryGetValue(stageBuildIndex, out data))
        {
            stages[stageBuildIndex] = new StageData();
            data = stages[stageBuildIndex];
        }
        return data;
    }
}
