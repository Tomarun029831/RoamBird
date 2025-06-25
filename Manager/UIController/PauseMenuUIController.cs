using UnityEngine;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvasObj;
    [SerializeField] Canvas pauseMenuCanvas;
    [SerializeField] GameObject pausePanel;
    private static PauseMenuUIController singleton;

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            Destroy(pauseMenuCanvasObj);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(pauseMenuCanvasObj);
    }

    // ========== Auto-Counter ==========

    // ========== REACTION of Keyboard ==========
    public void ToggleActiveOfPauseMenuCanvas() => pauseMenuCanvasObj.SetActive(true);
    public void ToggleActiveOfPausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
    }

    // ========== REACTION of GUI on display ==========
    public void OnHambugerButtonPressed() => pausePanel.SetActive(true);
    public void OnMainMenuButtonPressed()
    {
        pausePanel.SetActive(false);
        pauseMenuCanvasObj.SetActive(false);
        SceneChanger.LoadMainMenu();
    }
    public void OnCloseButtonPressed() => pausePanel.SetActive(false);
}
