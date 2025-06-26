using UnityEngine;
using TMPro;
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
        // username.text = "";
        // password.text = "";
    }

    public async Task OnLoginButtonPressed()
    {
        string username = this.Username;
        string password = this.Password;
        if (username == "" || password == "") { return; }

        string token = await UserAPIClient.DoLogin(plainUsername: username, plainPassword: password);
        TrackerAPIClient.Pull(token);
    }

    public void OnCreateAccountButtonPressed()
    {
        string username = this.Username;
        string password = this.Password;
        if (username == "" || password == "") { return; }

        UserAPIClient.CreateAcconut(plainUsername: username, plainPassword: password);
    }

    public void OnCloseButtonPressed()
    {
        username.text = "";
        password.text = "";
        loginForm.SetActive(false);
    }
}
