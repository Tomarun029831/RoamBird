using UnityEngine;

public class StartMenuUI : MonoBehaviour
{
    private PauseMenuUI pauseMenuUI;
    void Start()
    {
        pauseMenuUI = FindFirstObjectByType<PauseMenuUI>();
    }

    public void OnStartButtonPressed()
    {
        pauseMenuUI.ToggleActiveOfPauseMenuCanvas();
        SceneChanger.ChangeFarwardScene();
    }

    public void OnSelectButtonPressed()
    {

    }

    public void OnConfigButtonPressed()
    {

    }
}
