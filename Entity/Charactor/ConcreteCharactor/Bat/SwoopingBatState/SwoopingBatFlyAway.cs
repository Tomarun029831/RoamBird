using UnityEngine;

public class SwoopingBatFlyAway : SwoopingBatState
{
    private SwoopingBatFlyAway() { }

    private static class SingletonHolder { public static readonly SwoopingBatState instance = new SwoopingBatFlyAway(); }

    public static SwoopingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(SwoopingBat swoopingBat)
    {
        swoopingBat.internalTimer += Time.deltaTime;
        swoopingBat.MoveY(0.2f);
        if (swoopingBat.internalTimer < 1.6) return;
        TakeNextAction(swoopingBat);
    }

    public void TakeNextAction(SwoopingBat swoopingBat) => swoopingBat.Hide();

    public void Animate(Animator animator)
    {
        animator.SetBool("isFlaping", true);
        animator.SetBool("isSwooping", false);
    }
}
