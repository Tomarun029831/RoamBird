using UnityEngine;

public class SwoopingBatHover : SwoopingBatState
{
    private SwoopingBatHover() { }
    private static class SingletonHolder { public static readonly SwoopingBatState instance = new SwoopingBatHover(); }
    public static SwoopingBatState getInstance() => SingletonHolder.instance;

    public void FixedUpdate(SwoopingBat swoopingBat)
    {
        swoopingBat.internalTimer += Time.deltaTime;

        Vector2 rayDirection = swoopingBat.GetRestDirection() ? Vector2.right : Vector2.left;
        Vector2 raycastOrigin = new Vector2(swoopingBat.transform.position.x, swoopingBat.transform.position.y) + (rayDirection * 0.2f);
        RaycastHit2D raycastHit2D = Physics2D.Raycast(raycastOrigin, rayDirection, 0.7f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(swoopingBat.transform.position, rayDirection, Color.red, 0);

        if (raycastHit2D.collider != null) swoopingBat.Turn();
        swoopingBat.MoveX(0.2f);
        if (swoopingBat.internalTimer < 3) return;
        swoopingBat.internalTimer = 0;
        TakeNextAction(swoopingBat);
    }

    public void TakeNextAction(SwoopingBat swoopingBat) => swoopingBat.SetStateToFlyAway();

    public void Animate(Animator animator)
    {
        animator.SetBool("isFlaping", true);
        animator.SetBool("isSwooping", false);
    }
}
