using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public static class TrackerAPIClient
{
    private const string Url = ENV.TOMATECHAPI;

    public static void Push(string token, Dictionary<uint, StageData> trackingData)
    {
        var payload = new { mode = "PUSH", token = token, trackingDatas = trackingData };

        CoroutineRunner.Instance.StartCoroutine(
            APIRequestExecutor.PostJson(Url, payload, res => Debug.Log("Pushed: " + res))
        );
    }

    public static void Pull(string token, System.Action<Dictionary<uint, StageData>> onResult)
    {
        var payload = new { mode = "PULL", token = token };

        CoroutineRunner.Instance.StartCoroutine(
            APIRequestExecutor.PostJson(Url, payload, res =>
            {
                var tokenData = JsonConvert.DeserializeObject<TokenData>(res);
                onResult?.Invoke(tokenData.trackingDatas);
            })
        );
    }
}
