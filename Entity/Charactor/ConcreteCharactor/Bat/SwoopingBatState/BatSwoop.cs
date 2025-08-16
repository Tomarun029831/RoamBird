using UnityEngine;

public class BatSwoop : SwoopingBatState
{
    private BatSwoop() { }
    private static class SingletonHolder { public static readonly SwoopingBatState instance = new BatSwoop(); }
    public static SwoopingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(SwoopingBat swoopingBat)
    {
        swoopingBat.internalTimer += Time.deltaTime;
        bool isNotHoverTime = swoopingBat.internalTimer < 1.0f;
        if (isNotHoverTime) return;
        swoopingBat.internalTimer = 0;
        TakeNextAction(swoopingBat);
    }

    public void TakeNextAction(SwoopingBat swoopingBat) => swoopingBat.SetStateToHover();

    public void Animate(Animator animator)
    {
        animator.SetBool("isFlaping", false);
        animator.SetBool("isSwooping", true);
    }
}
