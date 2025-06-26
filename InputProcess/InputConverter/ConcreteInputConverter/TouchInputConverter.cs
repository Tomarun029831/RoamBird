using Assets.Scripts.Util.Delay;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using UnityEngine.EventSystems;
using UnityEngine;

public class TouchInputConverter : IInputConverter
{
    private InputHolder<int> touchHolder;

    public TouchInputConverter() => touchHolder = new InputHolder<int>(new List<int> { 0, 4 });

    public bool ConsumeSpace() => touchHolder.ConsumeInput(0);

    public bool ConsumeTab() => touchHolder.ConsumeInput(4);

    public void SyncInputState()
    {
        if (IsPointerOverGameObject()) { return; }

        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchHolder.HoldInput(0);
        }

        bool isFourFinguresTouched = Input.GetTouch(0).phase == TouchPhase.Began &&
            Input.GetTouch(1).phase == TouchPhase.Began &&
            Input.GetTouch(2).phase == TouchPhase.Began &&
            Input.GetTouch(3).phase == TouchPhase.Began;
        if (Input.touchCount == 4 && isFourFinguresTouched)
        {
            touchHolder.HoldInput(4);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPointerOverGameObject() => EventSystem.current.IsPointerOverGameObject();
}
