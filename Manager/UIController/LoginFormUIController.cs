using UnityEngine;
using TMPro;
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

    public async void OnLoginButtonPressedWrapper()
    {
        var (success, token, data) = await OnLoginButtonPressed();
    }

    public async void OnCreateAccountButtonPressedWrapper()
    {
        var (success, token) = await OnCreateAccountButtonPressed();
    }


    public async Task<(bool loginSucceeded, string token, TrackingData trackedData)> OnLoginButtonPressed()
    {
        string plainUsername = this.PlainUsername;
        string plainPassword = this.PlainPassword;
        if (plainUsername == "" || plainPassword == "") { return (false, null, null); }

        (bool loginSucceeded, string token) = await UserAPIClient.DoLogin(plainUsername: plainUsername, plainPassword: plainPassword);
        if (!loginSucceeded) return (false, null, null);

        (bool retrievalSucceeded, TrackingData trackedData) = await TrackerAPIClient.Pull(token);
        if (!retrievalSucceeded) return (false, null, null);

        return (retrievalSucceeded, token, trackedData);
    }

    public async Task<(bool creationSuccess, string token)> OnCreateAccountButtonPressed()
    {
        string plainUsername = this.PlainUsername;
        string plainPassword = this.PlainPassword;
        if (plainUsername == "" || plainPassword == "") return (false, null);

        (bool creationSuccess, string token) = await UserAPIClient.CreateAcconut(plainUsername: plainUsername, plainPassword: plainPassword);
        return (creationSuccess, token);
    }

    public void OnCloseButtonPressed() => loginForm.SetActive(false);
}
