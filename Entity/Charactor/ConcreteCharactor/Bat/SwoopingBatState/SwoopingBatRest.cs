using UnityEngine;

public class SwoopingBatRest : SwoopingBatState
{
    private SwoopingBatRest() { }

    private static class SingletonHolder
    {
        public static readonly SwoopingBatState instance = new SwoopingBatRest();
    }

    public static SwoopingBatState getInstance() => SingletonHolder.instance;

    public void Update(SwoopingBat swoopingBat)
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(swoopingBat.transform.position, Vector2.down, 6f, LayerMask.GetMask("VisiblePlayer"));
        if (raycastHit2D.collider == null)
        {
            return;
        }
        swoopingBat.State.TakeNextAction(swoopingBat);
        // Debug.DrawRay(swoopingBat.transform.position, 6f * Vector2.down, Color.red, 1);
        // Debug.Log(raycastHit2D.collider.name);
    }

    public void TakeNextAction(SwoopingBat swoopingBat)
    {
        swoopingBat.SetStateToSwoop();
    }

    public void Animate(Animator animator)
    {
        animator.SetBool("isFlaping", false);
        animator.SetBool("isSwooping", false);
    }
}
