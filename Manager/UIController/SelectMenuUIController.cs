using UnityEngine;

public class SelectMenuUIController : MonoBehaviour
{
    [SerializeField] private PauseMenuUIController pauseMenuUIController;
    [SerializeField] private GameObject selectMenu;

    public void Awake()
    {
        pauseMenuUIController = FindFirstObjectByType<PauseMenuUIController>();
    }

    public void Active()
    {
        selectMenu.SetActive(true);
    }

    public void OnEscPressed()
    {
        selectMenu.SetActive(false);
    }

    public void OnStage1Pressed()
    {
        pauseMenuUIController.ToggleActiveOfPauseMenuCanvas();
        SceneChanger.LoadSceneAt(1);
    }

    public void OnStage2Pressed()
    {
        pauseMenuUIController.ToggleActiveOfPauseMenuCanvas();
        SceneChanger.LoadSceneAt(2);
    }
}
