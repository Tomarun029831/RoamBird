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
        TimeSpan totalTimerProperty = track.totalTimer;
        TimeSpan TimerPerStageProperty = track.timerPerStage == TimeSpan.MaxValue ? TimeSpan.Zero : track.timerPerStage;

        totalCount.text = track.totalGoalCounter.ToString();
        streakCount.text = track.streakGoalCounter.ToString();
        totalTimer.text = totalTimerProperty.Seconds.ToString() + "." + track.totalTimer.Milliseconds.ToString() + "s";
        currentTimer.text = TimerPerStageProperty.Seconds.ToString() + "." + TimerPerStageProperty.Milliseconds.ToString() + "s";
    }
}
