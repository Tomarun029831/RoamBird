using System.Threading.Tasks;
using Newtonsoft.Json;

public static class UserAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;

    public static async Task<(bool isSuccess, string token)> DoLogin(string plainUsername, string plainPassword)
    {
        const string mode = "AUTHENTICATE";
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            username = plainUsername,
            password = hashedPassword
        };
        string stringfiedObj = JsonConvert.SerializeObject(jsonObj);

        (bool isSuccess, string receivedPayload) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedObj);
        if (!isSuccess) return (false, null);

        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(receivedPayload); }
        catch (System.Exception) { return (false, null); }
        if (responseJsonObj == null) return (false, null);

        bool apiSuccess = responseJsonObj.result == "success";
        string token = responseJsonObj.payload;

        return (apiSuccess, token);
    }

    public static async Task<(bool isSuccess, string token)> CreateAcconut(string plainUsername, string plainPassword)
    {
        const string mode = "CREATE";
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            username = plainUsername,
            password = hashedPassword
        };
        string stringfiedObj = JsonConvert.SerializeObject(jsonObj);

        (bool isSuccess, string receivedPayload) = await APIRequestExecutor.PostJson(url: APIURL, payload: stringfiedObj);
        if (!isSuccess) return (false, null);
        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(receivedPayload); }
        catch (System.Exception) { return (false, null); }
        if (responseJsonObj == null) return (false, null);

        bool apiSuccess = responseJsonObj.result == "success";
        string token = responseJsonObj.payload;

        return (apiSuccess, token);
    }

}
