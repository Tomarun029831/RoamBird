using UnityEngine;

public class SwoopingBatHover : SwoopingBatState
{
    private SwoopingBatHover() { }

    private static class SingletonHolder
    {
        public static readonly SwoopingBatState instance = new SwoopingBatHover();
    }

    public static SwoopingBatState getInstance() => SingletonHolder.instance;

    public void Update(SwoopingBat swoopingBat)
    {
        swoopingBat.internalTimer += Time.deltaTime;

        if (swoopingBat.internalTimer < 1.5)
        {
            swoopingBat.MoveX(-0.2f);
        }
        else
        {
            swoopingBat.MoveX(0.2f);
        }

        if (swoopingBat.internalTimer < 3) { return; }

        swoopingBat.internalTimer = 0;
        TakeNextAction(swoopingBat);
    }

    public void TakeNextAction(SwoopingBat swoopingBat)
    {
        swoopingBat.SetStateToFlyAway();
    }

    public void Animate(Animator animator)
    {
        animator.SetBool("isFlaping", true);
        animator.SetBool("isSwooping", false);
    }
}
