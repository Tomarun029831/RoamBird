[System.Serializable]
public class AccountData
{
    public AccountData(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
    public string username;
    public string password;
}
