using UnityEngine;

public class UnifiedInputProcessor : MonoBehaviour
{
    public Playable playable;
    [SerializeField] private PauseMenuUI pauseMenuUI;
    private IInputConverter inputConverter;
    private static UnifiedInputProcessor singleton;
    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);

        inputConverter = new KeyboardInputConverter();
    }

    void Update()
    {
        // ========== Get Input through converter ==========
        inputConverter.Update();

        // ========== Processing Unified Input ==========
        if (inputConverter.ConsumeTab())
        {
            pauseMenuUI.ToggleActiveOfPausePanel();
        }

        if (inputConverter.ConsumeSpace())
        {
            // playable
        }
    }
}
