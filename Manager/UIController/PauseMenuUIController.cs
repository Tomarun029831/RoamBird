using UnityEngine;
using System.Threading.Tasks;

public class PauseMenuUIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuCanvasObj;
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TrackInfoUIController trackInfoUIController;
    private static PauseMenuUIController singleton;
    private static bool IsPauseMenuOpen = false;

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

    private async Task UpdatePauseMenuInfoAsync()
    {
        while (IsPauseMenuOpen)
        {
            trackInfoUIController.UpdateTrackInfo(
                StageProgressionTracker.GetCurrentStageData()
            );

            await Task.Yield();
        }
    }

    // ========== REACTION of Keyboard ==========
    public void ToggleActiveOfPauseMenuCanvas() => pauseMenuCanvasObj.SetActive(true); // Init

    public void ToggleActiveOfPausePanel()
    {
        IsPauseMenuOpen = !IsPauseMenuOpen;
        pausePanel.SetActive(IsPauseMenuOpen);

        if (IsPauseMenuOpen)
            _ = UpdatePauseMenuInfoAsync();
    }

    // ========== REACTION of GUI on display ==========
    public void OnHambugerButtonPressed()
    {
        IsPauseMenuOpen = true;
        pausePanel.SetActive(true);
        _ = UpdatePauseMenuInfoAsync();
    }

    public void OnCloseButtonPressed()
    {
        IsPauseMenuOpen = false;
        pausePanel.SetActive(false);
    }

    public void OnMainMenuButtonPressed()
    {
        IsPauseMenuOpen = false;
        pausePanel.SetActive(false);
        pauseMenuCanvasObj.SetActive(false);
        SceneChanger.LoadMainMenu();
    }
}
