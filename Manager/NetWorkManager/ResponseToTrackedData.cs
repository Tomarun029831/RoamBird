public class ResponseToTrackedData
{
    public string result;
    public System.Collections.Generic.Dictionary<string, StageData> payload;
}

/*
=== PULL SUCCESS CASE0 ===
{
    "result": "success",
    "payload": {}
}

=== PULL SUCCESS CASE1 ===
{
    "result": "success",
    "payload": {
        "2": {
            "totalTimer": "10:25:40",
            "timerPerStage": "00:40:05",
            "totalGoalCounter": 18,
            "streakGoalCounter": 4
        },
        "4": {
            "totalTimer": "14:15:20",
            "timerPerStage": "00:35:33",
            "totalGoalCounter": 30,
            "streakGoalCounter": 5
        },
        "5": {
            "totalTimer": "16:40:10",
            "timerPerStage": "00:32:44",
            "totalGoalCounter": 27,
            "streakGoalCounter": 6
        }
    }
}

=== PULL SUCCESS CASE2 ===
{
    "result": "success",
    "payload": {
        "1": {
            "totalTimer": "08:15:30",
            "timerPerStage": "00:45:12",
            "totalGoalCounter": 12,
            "streakGoalCounter": 3
        },
        "2": {
            "totalTimer": "10:25:40",
            "timerPerStage": "00:40:05",
            "totalGoalCounter": 18,
            "streakGoalCounter": 4
        },
        "3": {
            "totalTimer": "12:05:55",
            "timerPerStage": "00:38:50",
            "totalGoalCounter": 25,
            "streakGoalCounter": 2
        },
        "4": {
            "totalTimer": "14:15:20",
            "timerPerStage": "00:35:33",
            "totalGoalCounter": 30,
            "streakGoalCounter": 5
        },
        "5": {
            "totalTimer": "16:40:10",
            "timerPerStage": "00:32:44",
            "totalGoalCounter": 27,
            "streakGoalCounter": 6
        }
    }
}

=== PULL FAILED CASE ===
{
    "result": "failed",
    "payload": {}
}

*/

/*
=== PUSH SUCCESS CASE0 ===
{
    "result": "success",
    "payload": {}
}


=== PUSH FAILED CASE1 ===
{
    "result": "failed",
    "payload": {}
}

*/
