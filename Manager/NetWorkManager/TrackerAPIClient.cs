using System.Threading.Tasks;
using Newtonsoft.Json;
using ZLinq;

public static class TrackerAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;
    private static string token = null;
    public static void SetToken(string token) => TrackerAPIClient.token = token;

    public static async Task<bool> Push(TrackingData trackingDatas)
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

            var dictUintKey = responseJsonObj.payload.AsValueEnumerable().ToDictionary(
                kvp => uint.Parse(kvp.Key),
                kvp => kvp.Value
            );

            TrackingData trackedData = new TrackingData(dictUintKey);
            return (true, trackedData);
        }
        catch (System.Exception) { return (false, null); }
    }
}
