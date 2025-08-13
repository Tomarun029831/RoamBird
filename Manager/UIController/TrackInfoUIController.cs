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
        totalTimer.text = (int)totalTimerProperty.TotalHours + "h" + totalTimerProperty.Minutes.ToString("D2") + "m" + totalTimerProperty.Seconds.ToString("D2") + "s";
        currentTimer.text = TimerPerStageProperty.TotalSeconds.ToString("F3") + "s";

        //         StageData stage = new StageData();
        //         stage.totalTimer = new TimeSpan(1, 2, 3);      // 1時間2分3秒
        //         stage.timerPerStage = TimeSpan.FromMilliseconds(12345); // 12.345秒
        //         stage.totalGoalCounter = 10;
        //         stage.streakGoalCounter = 3;
        //
        //         string json = Newtonsoft.Json.JsonConvert.SerializeObject(stage);
        //         Debug.Log(json);
        //         /*
        //         {"totalTimer":"01:02:03","timerPerStage":"00:00:12.3450000","totalGoalCounter":10,"streakGoalCounter":3}
        //         */
    }
}
