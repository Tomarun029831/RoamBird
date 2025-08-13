using System.Threading.Tasks;
using Newtonsoft.Json;

public static class AccountAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;

    public static async Task<bool> DoLogin(string plainUsername, string plainPassword)
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
        if (!isSuccess) return false;

        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(receivedPayload); }
        catch (System.Exception) { return false; }
        if (responseJsonObj == null) return false;

        bool apiSuccess = responseJsonObj.result == "success";
        string token = responseJsonObj.payload;
        TrackerAPIClient.SetToken(token);

        return apiSuccess;
    }

    public static async Task<bool> CreateAcconut(string plainUsername, string plainPassword)
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
        if (!isSuccess) return false;
        ResponseToAccount responseJsonObj = null;
        try { responseJsonObj = JsonConvert.DeserializeObject<ResponseToAccount>(receivedPayload); }
        catch (System.Exception) { return false; }
        if (responseJsonObj == null) return false;

        bool apiSuccess = responseJsonObj.result == "success";
        string token = responseJsonObj.payload;
        TrackerAPIClient.SetToken(token);

        return apiSuccess;
    }

}
