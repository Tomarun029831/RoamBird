using UnityEngine;

public class GlidingBatRest : GlidingBatState
{
    private GlidingBatRest() { }
    private static class SingletonHolder { public static readonly GlidingBatState instance = new GlidingBatRest(); }
    public static GlidingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(GlidingBat glidingBat)
    {
        Vector2 rayDirection = glidingBat.GetRestDirection() ? Vector2.left : Vector2.right;
        RaycastHit2D raycastHit2D = Physics2D.Raycast(glidingBat.transform.position, rayDirection, 9f, LayerMask.GetMask("VisiblePlayer"));
        Debug.DrawRay(glidingBat.transform.position, rayDirection, Color.red, 1f);
        bool isNotOnWall = raycastHit2D.collider == null;
        if (isNotOnWall) return;
        glidingBat.state.TakeNextAction(glidingBat);
    }

    public void TakeNextAction(GlidingBat glidingBat) => glidingBat.SetStateToGlide();

    public void Animate(Animator animator)
    {
        animator.SetBool("isGliding", false);
        animator.SetBool("isFlaping", false);
    }
}
