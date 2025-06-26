using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public static class HttpsConnector
{
    private const string TOMATECHAPIURL = ENV.TOMATECHAPI;
    private static string token = "";
    [System.Serializable]
    private class AccountData
    {
        public AccountData(string username, string password)
        {
            this.username = username;
            this.password = password;
        }
        public string username;
        public string password;
    }

    // [System.Serializable]
    // private class TokenData
    // {
    //     public TokenData(string token, Dictionary<uint, StageData> trackingDatas)
    //     {
    //         this.token = token;
    //         this.trackingDatas = trackingDatas;
    //     }
    //     public string token;
    //     public Dictionary<uint, StageData> trackingDatas;
    // }

    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject runnerObj = new GameObject("CoroutineRunner");
                    Object.DontDestroyOnLoad(runnerObj);
                    _instance = runnerObj.AddComponent<CoroutineRunner>();
                }
                return _instance;
            }
        }
    }

    public static void DoLogin(string plainUsername, string plainPassword)
    {
        const string mode = "AUTHENTICATE";
        const string url = TOMATECHAPIURL;
        string hashedPassword = ENV.computeHash(plainPassword);
        AccountData accountData = new AccountData(plainUsername, hashedPassword);

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                url: url,
                mode: mode,
                accountData: accountData
            )
        );
    }

    public static void CreateAcconut(string plainUsername, string plainPassword)
    {
        const string mode = "CREATE";
        const string url = TOMATECHAPIURL;
        string hashedPassword = ENV.computeHash(plainPassword);
        AccountData accountData = new AccountData(plainUsername, hashedPassword);

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                    url: url,
                    mode: mode,
                    accountData: accountData
                )
        );
    }

    public static void PushTrackerData(string token, Dictionary<uint, StageData> trackingDatas)
    {
        const string mode = "PUSH";
        const string url = TOMATECHAPIURL;

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                    url: url,
                    mode: mode,
                    token: token,
                    trackingDatas: trackingDatas
                )
        );
    }

    public static Dictionary<uint, StageData> PullTrackerData(string token) // TODO:
    {
        const string mode = "PULL";
        const string url = TOMATECHAPIURL;

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                    url: url,
                    mode: mode,
                    token: token
                )
        );
        return tokenData.trackingDatas;
    }

    private static IEnumerator PostForm(string url, string mode, AccountData accountData)
    {
        var data = new
        {
            mode = mode,
            accountData = accountData
        };

        string jsonPayload = JsonUtility.ToJson(data);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Failed: " + request.error);
            }
        }
    }

    private static IEnumerator PostForm(string url, string mode, string token, Dictionary<uint, StageData> trackingDatas)
    {
        var data = new
        {
            mode = mode,
            token = token,
        };

        string jsonPayload = JsonUtility.ToJson(data);
        byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonPayload);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            request.uploadHandler = new UploadHandlerRaw(jsonBytes);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Failed: " + request.error);
            }
        }
    }
}
