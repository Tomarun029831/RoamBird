using Assets.Scripts.Util.Delay;
using ZLinq;
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
        if (IsPointerOverGameObject()) return;
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began) touchHolder.HoldInput(0);
        bool isFourFinguresTouched = Input.touchCount == 4 && ValueEnumerable.Range(0, 4).All(n => Input.GetTouch(n).phase == TouchPhase.Began);
        if (isFourFinguresTouched) touchHolder.HoldInput(4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsPointerOverGameObject() => EventSystem.current.IsPointerOverGameObject();
}
