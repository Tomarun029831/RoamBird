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

        (bool isSucsess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: URL, payload: payload, token: token);
        // isSucsess = parse(response); // TODO:

        return (isSucsess);
    }

    public static async Task<(bool isSucsess, Dictionary<uint, StageData> trackedData)> Pull(string token)
    {
        Dictionary<uint, StageData> trackedData = null;
        var payload = new
        {
            mode = "PULL",
        };

        (bool isSucsess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: URL, payload: payload, token: token);
        // (isSucsess, trackingData) = parse(response); // TODO:

        return (isSucsess, trackedData);
    }
}
