using Assets.Scripts.Util.Delay;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine;

public class TouchInputConverter : IInputConverter
{
    private InputHolder<int> touchHolder;

    public TouchInputConverter()
    {
        touchHolder = new InputHolder<int>(new List<int> { 0, 4 });
    }

    public bool ConsumeSpace()
    {
        return touchHolder.ConsumeInput(0);
    }

    public bool ConsumeTab()
    {
        return touchHolder.ConsumeInput(4);
    }

    public void Update()
    {
        if (IsPointerOverGameObject()) { return; }

        if (Input.GetTouch(0).phase == TouchPhase.Began && Input.touchCount == 1)
        {
            touchHolder.HoldInput(0);
        }

        if (Input.GetTouch(0).phase == TouchPhase.Began &&
            Input.GetTouch(1).phase == TouchPhase.Began &&
            Input.GetTouch(2).phase == TouchPhase.Began &&
            Input.GetTouch(3).phase == TouchPhase.Began && Input.touchCount == 4)
        {
            touchHolder.HoldInput(4);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPointerOverGameObject() => EventSystem.current.IsPointerOverGameObject();
}
