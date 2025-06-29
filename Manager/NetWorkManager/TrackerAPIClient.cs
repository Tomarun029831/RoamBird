using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string URL = ENV.TOMATECHAPI;

    public static async Task<bool> Push(string token, Dictionary<uint, StageData> trackingData)
    {
        var payload = new
        {
            mode = "PUSH",
            trackingDatas = trackingData
        };

        (bool isSuccess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: URL, payload: payload, token: token);
        if (!isSuccess) { return false; }

        bool apiSuccess = false;
        try
        {
            ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response.downloadHandler.text);
            apiSuccess = responseJsonObj.isSuccess;
        }
        catch (System.Exception) { return false; }

        return (apiSuccess);
    }

    public static async Task<(bool isSucsess, Dictionary<uint, StageData> trackedData)> Pull(string token)
    {
        var payload = new
        {
            mode = "PULL",
        };

        (bool isSuccess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: URL, payload: payload, token: token);
        if (!isSuccess) { return (false, null); }

        bool apiSuccess = false;
        Dictionary<uint, StageData> trackedData = null;
        try
        {
            ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response.downloadHandler.text);
            apiSuccess = responseJsonObj.isSuccess;
            trackedData = responseJsonObj.trackedData;
        }
        catch (System.Exception) { return (false, null); }

        return (apiSuccess, trackedData);
    }
}
