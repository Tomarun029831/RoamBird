using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System.Security.Cryptography;

public static class HttpsConnector
{
    private const string TomaTechAPIURL = "";

    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    var runnerObj = new GameObject("CoroutineRunner");
                    Object.DontDestroyOnLoad(runnerObj);
                    _instance = runnerObj.AddComponent<CoroutineRunner>();
                }
                return _instance;
            }
        }
    }

    public static void doLogin(string plainUsername, string plainPassword)
    {
        const string mode = "AUTHENTICATE";
        const string url = TomaTechAPIURL;

        string hashedPassword = ComputeSHA256(plainPassword);

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                url,
                mode,
                plainUsername,
                hashedPassword
            )
        );
    }

    public static void createAcconut(string plainUsername, string plainPassword)
    {
        const string mode = "CREATE";
        const string url = TomaTechAPIURL;
;
        string hashedPassword = ComputeSHA256(plainPassword);

        CoroutineRunner.Instance.StartCoroutine(
            PostForm(
                    url,
                    mode,
                    plainUsername,
                    hashedPassword
                )
        );
    }

    // public static void pushTrackerData(StageProgressionTracker stageProgressionTracker)
    // {
    //     const string mode = "PUSH";
    //     const string url = TomaTechAPIURL;


    //     // CoroutineRunner.Instance.StartCoroutine(
    //     //     PostForm(
    //     //             url,
    //     //             mode,
    //     //         )
    //     // );
    // }

    // public static void pullTrackerData(StageProgressionTracker stageProgressionTracker)
    // {
    //     const string mode = "PULL";
    //     const string url = TomaTechAPIURL;


    //     // CoroutineRunner.Instance.StartCoroutine(
    //     //     PostForm(
    //     //             url,
    //     //             mode,
    //     //         )
    //     // );
    // }

    private static IEnumerator PostForm(string url, string mode, string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("mode", mode);
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Login Success: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Login Failed: " + request.error);
            }
        }
    }

    public static string ComputeSHA256(string input) // TODO: more Secure
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] data = Encoding.UTF8.GetBytes(input);
            byte[] hash = sha256.ComputeHash(data);
            return System.BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
