using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System.Threading.Tasks;

public static class APIRequestExecutor
{
    public static async Task<(bool isSuccess, UnityWebRequest response)> PostJson(string url, string payload, string token = null)
    {
        byte[] jsonBytes = Encoding.UTF8.GetBytes(payload);

        using (UnityWebRequest req = new UnityWebRequest(url, "POST"))
        {
            req.uploadHandler = new UploadHandlerRaw(jsonBytes);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");
            if (!string.IsNullOrWhiteSpace(token))
            {
                req.SetRequestHeader("Authorization", $"Bearer {token}");
            }

            await req.SendWebRequest();

            if (req.result != UnityWebRequest.Result.Success)
                return (false, null);

            return (true, req);
        }
    }
}
