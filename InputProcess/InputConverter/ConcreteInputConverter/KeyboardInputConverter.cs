using Assets.Scripts.Util.Delay;
using System.Collections.Generic;

using UnityEngine;

public class KeyboardInputConverter : IInputConverter
{
    private InputHolder<KeyCode> KeyboardHolder;

    public KeyboardInputConverter()
    {
        KeyboardHolder = new InputHolder<KeyCode>(new List<KeyCode> { KeyCode.Space, KeyCode.Tab });
    }

    public bool ConsumeJump()
    {
        return KeyboardHolder.ConsumeInput(KeyCode.Space);
    }

    public bool ConsumeTab()
    {
        return KeyboardHolder.ConsumeInput(KeyCode.Tab);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            KeyboardHolder.HoldInput(KeyCode.Space);
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            KeyboardHolder.HoldInput(KeyCode.Tab);
        }
    }
}
