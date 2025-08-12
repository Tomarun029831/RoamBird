using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;

    public static async Task<bool> Push(string token, Dictionary<uint, StageData> trackingData)
    {
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

        return (apiSuccess);
    }

    public static async Task<(bool isSucsess, Dictionary<uint, StageData> trackedData)> Pull(string token)
    {
        var payload = new
        {
            mode = "PULL",
            token = token
        };
        string stringfiedPayload = JsonConvert.SerializeObject(payload);

        (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedPayload);
        if (!isSuccess) return (false, null);

        bool apiSuccess = false;
        Dictionary<uint, StageData> trackedData = null;
        try
        {
            ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response);
            apiSuccess = responseJsonObj.result == "success";
            trackedData = responseJsonObj.trackedData;
        }
        catch (System.Exception) { return (false, null); }

        return (apiSuccess, trackedData);
    }
}
