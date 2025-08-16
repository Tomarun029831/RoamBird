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
        TimeSpan TimerPerStageProperty = track.timerPerStage;

        totalCount.text = track.totalGoalCounter.ToString();
        streakCount.text = track.streakGoalCounter.ToString();
        totalTimer.text = (int)totalTimerProperty.TotalHours + "h" + totalTimerProperty.Minutes.ToString("D2") + "m" + totalTimerProperty.Seconds.ToString("D2") + "s";
        currentTimer.text = TimerPerStageProperty.TotalSeconds.ToString("F3") + "s";
    }
}
