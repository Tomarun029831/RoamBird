using UnityEngine;

public class StartMenuUIControllerController : MonoBehaviour
{
    [SerializeField] private PauseMenuUIController pauseMenuUIController;
    [SerializeField] private SelectMenuUIController selectMenuUIController;
    // [SerializeField] private LoginFormUIController loginFormUIController;

    public void Awake()
    {
        pauseMenuUIController = FindFirstObjectByType<PauseMenuUIController>();
    }

    public void OnStartButtonPressed()
    {
        pauseMenuUIController.ToggleActiveOfPauseMenuCanvas();
        SceneChanger.ChangeFarwardScene();
    }

    public void OnSelectButtonPressed()
    {
        selectMenuUIController.Active();
    }

    public void OnButtonToLoginForm()
    {
        // loginFormUIController.Active();
    }

    public void OnConfigButtonPressed()
    {

    }
}
