using System.Collections.Generic;
using System;
using System.Diagnostics;

public class StageData
{
    public TimeSpan TotalTimer = TimeSpan.Zero;
    public TimeSpan TimerPerStage = TimeSpan.MaxValue;
    public Stopwatch CurrentTimer = new();
    public uint TotalGoalCounter = 0;
    public uint StreakGoalCounter = 0;
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
        data.CurrentTimer.Restart();
        state = State.InTracking;
    }

    public static void StopTrack(bool goalAchieved)
    {
        StageData data = GetStageData(currentStageBuildIndex);
        if (data == null || state != State.InTracking) return;
        data.CurrentTimer.Stop();

        if (goalAchieved)
        {
            // === Streak ===
            data.StreakGoalCounter++;
            data.TimerPerStage = data.TimerPerStage > data.CurrentTimer.Elapsed ? data.CurrentTimer.Elapsed : data.TimerPerStage;
            // === Total ===
            data.TotalGoalCounter++;
        }
        else
        {
            data.StreakGoalCounter = 0;
        }
        data.TotalTimer += data.CurrentTimer.Elapsed;

        state = State.InStop;
    }

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
