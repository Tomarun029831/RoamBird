using UnityEngine;
using TMPro;
using System.Threading.Tasks;

public class LoginFormUIController : MonoBehaviour
{
    [SerializeField] private NoticeUIController noticeUIController;
    [SerializeField] private GameObject loginForm;
    [SerializeField] private TMP_InputField plainUsername;
    [SerializeField] private TMP_InputField plainPassword;
    private string PlainUsername => plainUsername.text;
    private string PlainPassword => plainPassword.text;

    public void Active()
    {
        loginForm.SetActive(true);
        plainUsername.text = "";
        plainPassword.text = "";
    }

    public void OnCloseButtonPressed() => loginForm.SetActive(false);

    public async void OnLoginButtonPressedWrapper()
    {
        if (string.IsNullOrWhiteSpace(PlainUsername) || string.IsNullOrWhiteSpace(PlainPassword)) return;
        noticeUIController.PopLoginProgressNotice();
        var (success, receivedTrackingData) = await OnLoginButtonPressed(PlainUsername, PlainPassword);
        if (!success) { noticeUIController.PopLoginFailedNotice(); return; }
        noticeUIController.PopLoginSuccessNotice();
        StageProgressionTracker.SetTrackingData(receivedTrackingData);
    }

    public async void OnCreateAccountButtonPressedWrapper()
    {
        if (string.IsNullOrWhiteSpace(PlainUsername) || string.IsNullOrWhiteSpace(PlainPassword)) return;
        noticeUIController.PopLoginProgressNotice();
        var success = await OnCreateAccountButtonPressed(PlainUsername, PlainPassword);
        if (!success) { noticeUIController.PopLoginFailedNotice(); return; }
        noticeUIController.PopLoginSuccessNotice();
    }

    public async Task<(bool loginSucceeded, TrackingData trackedData)> OnLoginButtonPressed(string plainUsername, string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainUsername) || string.IsNullOrWhiteSpace(plainPassword)) return (false, null);
        bool loginSucceeded = await AccountAPIClient.DoLogin(plainUsername: plainUsername, plainPassword: plainPassword);
        if (!loginSucceeded) return (false, null);

        (bool retrievalSucceeded, TrackingData trackedData) = await TrackerAPIClient.Pull();
        if (!retrievalSucceeded) return (false, null);

        return (retrievalSucceeded, trackedData);
    }

    public async Task<bool> OnCreateAccountButtonPressed(string plainUsername, string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainUsername) || string.IsNullOrWhiteSpace(plainPassword)) return false;

        bool creationSuccess = await AccountAPIClient.CreateAcconut(plainUsername: plainUsername, plainPassword: plainPassword);
        return creationSuccess;
    }

}
