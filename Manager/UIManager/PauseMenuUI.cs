using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas;
    [SerializeField] GameObject pausePanel;
    private static PauseMenuUI singleton;
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseMenuCanvas);
    }


    // ========== REACTION of Keyboard ==========
    public void ToggleActiveOfPauseMenuCanvas() => pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
    public void ToggleActiveOfPausePanel()
    {
        if (pauseMenuCanvas.activeSelf) pausePanel.SetActive(!pausePanel.activeSelf);
    }

    // ========== REACTION of GUI on display ==========
    public void OnHambugerButtonPressed() => pausePanel.SetActive(true);
    public void OnMainMenuButtonPressed()
    {
        pausePanel.SetActive(false);
        pauseMenuCanvas.SetActive(false);
        SceneChanger.LoadMainMenu();
    }
    public void OnCloseButtonPressed() => pausePanel.SetActive(false);
}
