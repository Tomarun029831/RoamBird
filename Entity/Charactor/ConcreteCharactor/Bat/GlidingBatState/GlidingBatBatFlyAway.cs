using UnityEngine;

public class GlidingBatFlyAway : GlidingBatState
{
    private GlidingBatFlyAway() { }
    private static class SingletonHolder { public static readonly GlidingBatState instance = new GlidingBatFlyAway(); }
    public static GlidingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(GlidingBat glidingBat)
    {
        glidingBat.internalTimer += Time.deltaTime;
        glidingBat.MoveY(0.2f);
        bool isNotDisappearTime = glidingBat.internalTimer < 1.6;
        if (isNotDisappearTime) return;
        TakeNextAction(glidingBat);
    }

    public void TakeNextAction(GlidingBat glidingBat) => glidingBat.Hide();

    public void Animate(Animator animator)
    {
        animator.SetBool("isGliding", false);
        animator.SetBool("isFlaping", true);
    }
}
