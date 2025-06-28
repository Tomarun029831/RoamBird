using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LoginFormUIController : MonoBehaviour
{
    [SerializeField] private GameObject loginForm;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    public string Username => username.text;
    public string Password => password.text;

    public void Active()
    {
        loginForm.SetActive(true);
        username.text = "";
        password.text = "";
    }

    public async Task<(bool isSuccess, string token, Dictionary<uint, StageData> trackedData)> OnLoginButtonPressed()
    {
        string username = this.Username;
        string password = this.Password;
        if (username == "" || password == "") { return (false, null, null); }

        (bool loginSucceeded, string token) = await UserAPIClient.DoLogin(plainUsername: username, plainPassword: password);
        if (!loginSucceeded) { Debug.Log("Login was failed."); return (false, null, null); }

        (bool retrievalSucceeded, Dictionary<uint, StageData> trackedData) = await TrackerAPIClient.Pull(token);
        if (!retrievalSucceeded) { Debug.Log("Pulling track-data was failed."); return (false, null, null); }

        return (retrievalSucceeded, token, trackedData);
    }

    public async Task<(bool isSuccess, string token)> OnCreateAccountButtonPressed()
    {
        string username = this.Username;
        string password = this.Password;
        if (username == "" || password == "") { return (false, null); }

        (bool isSuccess, string token) = await UserAPIClient.CreateAcconut(plainUsername: username, plainPassword: password);
        return (isSuccess, token);
    }

    public void OnCloseButtonPressed() => loginForm.SetActive(false);
}
