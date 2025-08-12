using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
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


    public static async Task<(bool isSuccess, TrackingData trackedData)> Pull(string token)
    {
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

    // public static async Task<(bool isSucsess, TrackingData trackedData)> Pull(string token)
    // {
    //     var payload = new
    //     {
    //         mode = "PULL",
    //         token = token
    //     };
    //     string stringfiedPayload = JsonConvert.SerializeObject(payload);
    //
    //     (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedPayload);
    //     if (!isSuccess) return (false, null);
    //
    //     bool apiSuccess = false;
    //     TrackingData trackedData = null;
    //     try
    //     {
    //         ResponseToTrackedData responseJsonObj = JsonConvert.DeserializeObject<ResponseToTrackedData>(response);
    //         apiSuccess = responseJsonObj.result == "success";
    //         trackedData = responseJsonObj.payload;
    //     }
    //     catch (System.Exception) { return (false, null); }
    //
    //     return (apiSuccess, trackedData);
    // }
}
