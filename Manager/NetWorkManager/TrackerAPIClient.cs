using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;
    private static string token = null;

    public static void SetToken(string token) => TrackerAPIClient.token = token;

    public static async Task<bool> Push(Dictionary<uint, StageData> trackingData)
    {
        if (string.IsNullOrWhiteSpace(token)) return false;
        var payload = new
        {
            mode = "PUSH",
            token = token,
            trackingDatas = trackingData
        };
        string stringfiedPayload = JsonConvert.SerializeObject(payload);

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
        var payload = new
        {
            mode = "PULL",
            token = token
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
