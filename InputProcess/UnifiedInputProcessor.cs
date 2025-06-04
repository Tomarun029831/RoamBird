using UnityEngine;

public class UnifiedInputProcessor : MonoBehaviour
{
    public Playable playable;
    [SerializeField] private PauseMenuUI pauseMenuUI;
    private IInputConverter[] inputConverter;
    private static UnifiedInputProcessor singleton;

    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            inputConverter = new IInputConverter[] {new KeyboardInputConverter(), new TouchInputConverter()};
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
        foreach(IInputConverter converter in inputConverter)
        {
            converter.Update();

        // ========== Processing Unified Input ==========
            if (!converter.ConsumeTab()){continue;}
            pauseMenuUI.ToggleActiveOfPausePanel();
        }
    }

    void FixedUpdate()
    {
        foreach(IInputConverter converter in inputConverter)
        {
            if(!converter.ConsumeSpace()){continue;}
            playable.Execute(Playable.Bind.Space);
        }
    }
}
