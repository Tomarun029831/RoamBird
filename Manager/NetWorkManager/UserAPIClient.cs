using System.Threading.Tasks;

public static class UserAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;

    public static async Task<(bool isSuccess, string token)> DoLogin(string plainUsername, string plainPassword)
    {
        string token = "";
        const string mode = "AUTHENTICATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = new { username = plainUsername, password = hashedPassword }
        };

        (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: url, payload: jsonObj);
        // token = extractToken(response); // TODO:

        return (isSuccess, token);
    }

    public static async Task<(bool isSuccess, string token)> CreateAcconut(string plainUsername, string plainPassword)
    {
        string token = "";
        const string mode = "CREATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = new { username = plainUsername, password = hashedPassword }
        };

        (bool isSuccess, string response) = await APIRequestExecutor.PostJson(url: url, payload: jsonObj);
        // token = extractToken(response); // TODO: 

        return (isSuccess, token);
    }
}
