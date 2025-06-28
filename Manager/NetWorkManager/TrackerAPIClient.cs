using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string URL = ENV.TOMATECHAPI;

    public static async Task<bool> Push(string token, Dictionary<uint, StageData> trackingData)
    {
        var payload = new
        {
            mode = "PUSH",
            token = token,
            trackingDatas = trackingData
        };

        (bool isSucsess, string response) = await APIRequestExecutor.PostJson(URL, payload);
        // isSucsess = parse(response); // TODO:

        return (isSucsess);
    }

    public static async Task<(bool isSucsess, Dictionary<uint, StageData> trackedData)> Pull(string token)
    {
        Dictionary<uint, StageData> trackedData = null;
        var payload = new
        {
            mode = "PULL",
            token = token
        };

        (bool isSucsess, string response) = await APIRequestExecutor.PostJson(URL, payload);
        // (isSucsess, trackingData) = parse(response); // TODO:

        return (isSucsess, trackedData);
    }
}
