using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;
    private static string token = null;
    public static void SetToken(string token) => TrackerAPIClient.token = token;

    public static async Task<bool> Push(TrackingData trackingDatas) // FIXME:
    {
        if (string.IsNullOrWhiteSpace(token)) return false;
        string mode = "PUSH";
        string stringfiedTrackingDatas = Newtonsoft.Json.JsonConvert.SerializeObject(trackingDatas);
        var payload = new
        {
            mode = mode,
            token = token,
            trackingDatas = trackingDatas,
            checksum = ENV.ComputeHash(mode + token + stringfiedTrackingDatas)
        };
        UnityEngine.Debug.Log("Push-checksum: " + payload.checksum);
        string stringfiedPayload = JsonConvert.SerializeObject(payload);
        UnityEngine.Debug.Log(stringfiedTrackingDatas);
        // {"1":{"totalTimer":"20:45:53.5671627","timerPerStage":"00:00:19.0241055","totalGoalCounter":1,"streakGoalCounter":0},"2":{"totalTimer":"17:05:20","timerPerStage":"17:45:10","totalGoalCounter":60,"streakGoalCounter":19}}
        UnityEngine.Debug.Log(stringfiedPayload);
        // {"mode":"PUSH","token":"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImR1bW15IiwiaWF0IjoxNzU1MTM1OTAwLCJleHAiOjE3NTUxMzk1MDB9.RYVPaTIzCNv-OU2I5m09mRUb0TxQNYlbzWq9o4pskmY","trackingDatas":{"1":{"totalTimer":"20:45:53.5648510","timerPerStage":"00:00:19.0241055","totalGoalCounter":1,"streakGoalCounter":0},"2":{"totalTimer":"17:05:20","timerPerStage":"17:45:10","totalGoalCounter":60,"streakGoalCounter":19}},"checksum":"b66aea4c2247c1af7a56790025b5d94eb4a6c5c49e20bcbfb18bfc2dd8280828"}

        (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedPayload);
        if (!isSuccess) return false;

        bool apiSuccess = false;
        try
        {
            ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response);
            apiSuccess = responseJsonObj.result == "success";
        }
        catch (System.Exception) { return false; }

        return apiSuccess;
    }

    public static async Task<(bool isSuccess, TrackingData trackedData)> Pull()
    {
        if (string.IsNullOrWhiteSpace(token)) return (false, null);
        string mode = "PULL";
        var payload = new
        {
            mode = "PULL",
            token = token,
            checksum = ENV.ComputeHash(mode + token)
        };
        string stringfiedPayload = JsonConvert.SerializeObject(payload);

        (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedPayload);
        if (!isSuccess) return (false, null);

        try
        {
            ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response);
            if (responseJsonObj == null || responseJsonObj.result != "success" || responseJsonObj.payload == null)
                return (false, null);

            var dictUintKey = responseJsonObj.payload.ToDictionary(
                kvp => uint.Parse(kvp.Key),
                kvp => kvp.Value
            );

            TrackingData trackedData = new TrackingData(dictUintKey);
            return (true, trackedData);
        }
        catch (System.Exception) { return (false, null); }
    }
}
