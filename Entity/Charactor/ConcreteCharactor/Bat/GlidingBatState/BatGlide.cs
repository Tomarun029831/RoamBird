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
        bool restDirection = glidingBat.GetRestDirection();
        glidingBat.internalTimer += Time.deltaTime;

        Vector2 rayDirection = glidingBat.GetRestDirection() ? Vector2.right : Vector2.left;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(glidingBat.transform.position, rayDirection, 0.7f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(glidingBat.transform.position, rayDirection, Color.red, 0);

        glidingBat.Move(new Vector2(0.2f * Mathf.Abs(Mathf.Sin(glidingBat.internalTimer % 1)), -0.05f));

        if (raycastHit2D.collider != null) { glidingBat.Turn(); }

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
