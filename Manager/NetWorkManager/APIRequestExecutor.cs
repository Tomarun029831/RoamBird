using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

public static class APIRequestExecutor
{
    public static async Task<(bool isSuccess, string response)> PostJson(string url, object payload)
    {
        string json = JsonConvert.SerializeObject(payload);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(json);

        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(jsonBytes);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            await req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                return (false, null);

            return (true, req.downloadHandler.text);
        }
    }
}
