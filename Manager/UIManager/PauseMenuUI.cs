using UnityEngine;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] Canvas pauseMenuCanvas;
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
    }

    // ========== REACTION of Keyboard ==========
    public void ToggleActiveOfPauseMenuCanvas() => pauseMenuCanvas.enabled = !pauseMenuCanvas.enabled;
    public void ToggleActiveOfPausePanel()
    {
        if (pauseMenuCanvas.enabled) pausePanel.SetActive(!pausePanel.activeSelf);
    }

    // ========== REACTION of GUI on display ==========
    public void OnHambugerButtonPressed() => pausePanel.SetActive(true);
    public void OnMainMenuButtonPressed()
    {
        pausePanel.SetActive(false);
        pauseMenuCanvas.enabled = false;
        SceneChanger.LoadMainMenu();
    }
    public void OnCloseButtonPressed() => pausePanel.SetActive(false);
}
