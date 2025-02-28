using UnityEngine;

public class UnifiedInputProcessor : MonoBehaviour
{
    public Playable playable;
    private IInputConverter inputConverter;

    void Awake()
    {
        inputConverter = new KeyboardInputConverter();
    }

    void Update()
    {
        // ========== get Input through converter ==========
        inputConverter.Update();

        // ========== Processing Unified Input ==========
        if (inputConverter.ConsumeTab())
        {
            // toggle pauseMenu UI
        }

        if (inputConverter.ConsumeJump())
        {
            // playable
        }
    }
}
