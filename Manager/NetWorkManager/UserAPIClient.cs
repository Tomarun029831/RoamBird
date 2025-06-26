public static class UserAPIClient
{
    private const string APIURL = ENV.TOMATECHAPI;
    private static string _token = "";

    public static void DoLogin(string plainUsername, string plainPassword)
    {
        const string mode = "AUTHENTICATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        AccountData accountData = new AccountData(plainUsername, hashedPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = accountData
        };

        CoroutineRunner.Instance.StartCoroutine(
            APIRequestExecutor.PostJson(
                url: url,
                payload: jsonObj
            )
        );
    }

    public static void CreateAcconut(string plainUsername, string plainPassword)
    {
        const string mode = "CREATE";
        const string url = APIURL;
        string hashedPassword = ENV.ComputeHash(plainPassword);
        AccountData accountData = new AccountData(plainUsername, hashedPassword);
        var jsonObj = new
        {
            mode = mode,
            accountData = accountData
        };

        CoroutineRunner.Instance.StartCoroutine(
            APIRequestExecutor.PostJson(
                    url: url,
                    payload: jsonObj
                )
        );
    }
}
