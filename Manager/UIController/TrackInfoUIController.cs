using UnityEngine;
using TMPro;
using System;

public class TrackInfoUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalCount;
    [SerializeField] private TextMeshProUGUI streakCount;
    [SerializeField] private TextMeshProUGUI totalTimer;
    [SerializeField] private TextMeshProUGUI currentTimer;

    private static TrackInfoUIController singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateTrackInfo(StageData track)
    {
        TimeSpan totalTimerProperty = track.TotalTimer;
        TimeSpan TimerPerStageProperty = track.TimerPerStage == TimeSpan.MaxValue ? TimeSpan.Zero : track.TimerPerStage;

        totalCount.text = track.TotalGoalCounter.ToString();
        streakCount.text = track.StreakGoalCounter.ToString();
        totalTimer.text = totalTimerProperty.Seconds.ToString() + "." + track.TotalTimer.Milliseconds.ToString() + "s";
        currentTimer.text = TimerPerStageProperty.Seconds.ToString() + "." + TimerPerStageProperty.Milliseconds.ToString() + "s";
    }
}
