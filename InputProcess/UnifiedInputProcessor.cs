using UnityEngine;

public class UnifiedInputProcessor : MonoBehaviour
{
    public Playable playable;
    [SerializeField] private PauseMenuUIController pauseMenuUIController;
    private IInputConverter[] inputConverter;
    private static UnifiedInputProcessor singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            inputConverter = new IInputConverter[] { new KeyboardInputConverter(), new TouchInputConverter() };
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            this.pauseMenuUIController = singleton.pauseMenuUIController;
            this.inputConverter = singleton.inputConverter;
            Destroy(singleton.gameObject);
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        // ========== Get Input through converter ==========
        foreach (IInputConverter converter in inputConverter)
        {
            converter.Update();
            // ========== Processing Unified Input ==========
            if (!converter.ConsumeTab()) { continue; }
            pauseMenuUIController.ToggleActiveOfPausePanel();
        }
    }

    void FixedUpdate()
    {
        foreach (IInputConverter converter in inputConverter)
        {
            if (!converter.ConsumeSpace()) { continue; }
            playable.Execute(Playable.Bind.Space);

            if (StageProgressionTracker.state == StageProgressionTracker.State.InReady) { StageProgressionTracker.StartTrack(); }
        }
    }
}
