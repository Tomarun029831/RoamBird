using UnityEngine;
using TMPro;

public class LoginFormUIController : MonoBehaviour
{
    [SerializeField] private GameObject loginForm;
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;

    public void Active()
    {
        loginForm.SetActive(true);
        username.text = "";
        password.text = "";
    }

    public string getUsername()
    {
        return username.text;
    }

    public string getPassword()
    {
        return password.text;
    }

    public void OnLoginButtonPressed()
    {
        string username = this.getUsername();
        string password = this.getPassword();
        if (username == "" || password == "") { return; }

        // HttpsConnector.doLogin(plainUsername:username, plainPassword:password);
    }

    public void OnCreateAccountButtonPressed()
    {
        string username = this.getUsername();
        string password = this.getPassword();
        if (username == "" || password == "") { return; }

        // HttpsConnector.createAcconut(plainUsername:username, plainPassword:password);
    }

    public void OnCloseButtonPressed()
    {
        username.text = "";
        password.text = "";
        loginForm.SetActive(false);
    }

}
