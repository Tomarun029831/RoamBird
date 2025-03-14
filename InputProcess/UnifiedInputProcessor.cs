using UnityEngine;

public class UnifiedInputProcessor : MonoBehaviour
{
    public Playable playable;
    [SerializeField] private PauseMenuUI pauseMenuUI;
    private IInputConverter inputConverter;
    private static UnifiedInputProcessor singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            inputConverter = new KeyboardInputConverter();
            // inputConverter = new TouchInputConverter();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            this.pauseMenuUI = singleton.pauseMenuUI;
            this.inputConverter = singleton.inputConverter;
            Destroy(singleton.gameObject);
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
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
    }

    void FixedUpdate()
    {
        if (inputConverter.ConsumeSpace())
        {
            playable.Execute(Playable.Bind.Space);
        }
    }
}
