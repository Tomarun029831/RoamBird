using UnityEngine.Networking;
using System.Threading.Tasks;
using Newtonsoft.Json;

public static class UserAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;

    public static async Task<(bool isSuccess, string token)> DoLogin(string plainUsername, string plainPassword)
    {
        const string mode = "AUTHENTICATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = new { username = plainUsername, password = hashedPassword }
        };

        (bool isSuccess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: url, payload: jsonObj);
        if (!isSuccess) { return (false, null); }

        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(response.downloadHandler.text); }
        catch (System.Exception) { return (false, null); }
        if (responseJsonObj == null) { return (false, null); }

        bool apiSuccess = responseJsonObj.isSuccess;
        string token = responseJsonObj.token;

        return (apiSuccess, token);
    }

    public static async Task<(bool isSuccess, string token)> CreateAcconut(string plainUsername, string plainPassword)
    {
        const string mode = "CREATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = new { username = plainUsername, password = hashedPassword }
        };

        (bool isSuccess, UnityWebRequest response) = await APIRequestExecutor.PostJson(url: url, payload: jsonObj);
        if (!isSuccess) { return (false, null); }

        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(response.downloadHandler.text); }
        catch (System.Exception) { return (false, null); }
        if (responseJsonObj == null) { return (false, null); }

        bool apiSuccess = responseJsonObj.isSuccess;
        string token = responseJsonObj.token;

        return (apiSuccess, token);
    }

}
