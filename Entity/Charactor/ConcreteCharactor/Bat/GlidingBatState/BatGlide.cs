using UnityEngine;

public class BatGlide : GlidingBatState
{
    private BatGlide() { }

    private static class SingletonHolder
    {
        public static readonly GlidingBatState instance = new BatGlide();
    }

    public static GlidingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(GlidingBat glidingBat)
    {
        glidingBat.internalTimer += Time.deltaTime;

        if (glidingBat.internalTimer < 1.5)
        {
            glidingBat.MoveX(-0.2f);
        }
        else
        {
            glidingBat.MoveX(0.2f);
        }

        if (glidingBat.internalTimer < 3) { return; }

        glidingBat.internalTimer = 0;
        TakeNextAction(glidingBat);
    }

    public void TakeNextAction(GlidingBat glidingBat)
    {
        glidingBat.SetStateToFlyAway();
    }

    public void Animate(Animator animator)
    {
        animator.SetBool("isGliding", true);
        animator.SetBool("isFlaping", false);
    }
}
