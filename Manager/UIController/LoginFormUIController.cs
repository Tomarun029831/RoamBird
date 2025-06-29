using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LoginFormUIController : MonoBehaviour
{
    [SerializeField] private GameObject loginForm;
    [SerializeField] private TMP_InputField plainUsername;
    [SerializeField] private TMP_InputField plainPassword;
    public string PlainUsername => plainUsername.text;
    public string PlainPassword => plainPassword.text;

    public void Active()
    {
        loginForm.SetActive(true);
        plainUsername.text = "";
        plainPassword.text = "";
    }

    public async Task<(bool loginSucceeded, string token, Dictionary<uint, StageData> trackedData)> OnLoginButtonPressed()
    {
        string plainUsername = this.PlainUsername;
        string plainPassword = this.PlainPassword;
        if (plainUsername == "" || plainPassword == "") { return (false, null, null); }

        (bool loginSucceeded, string token) = await UserAPIClient.DoLogin(plainUsername: plainUsername, plainPassword: plainPassword);
        if (!loginSucceeded) { Debug.Log("Login was failed."); return (false, null, null); }

        (bool retrievalSucceeded, Dictionary<uint, StageData> trackedData) = await TrackerAPIClient.Pull(token); // HACK:
        if (!retrievalSucceeded) { Debug.Log("Pulling track-data was failed."); return (false, null, null); }

        return (retrievalSucceeded, token, trackedData);
    }

    public async Task<(bool creationSuccess, string token)> OnCreateAccountButtonPressed()
    {
        string plainUsername = this.PlainUsername;
        string plainPassword = this.PlainPassword;
        if (plainUsername == "" || plainPassword == "") { return (false, null); }

        (bool creationSuccess, string token) = await UserAPIClient.CreateAcconut(plainUsername: plainUsername, plainPassword: plainPassword);
        return (creationSuccess, token);
    }

    public void OnCloseButtonPressed() => loginForm.SetActive(false);
}
