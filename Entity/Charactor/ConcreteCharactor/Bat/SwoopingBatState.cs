using UnityEngine;

public interface SwoopingBatState
{
    public void FixedUpdate(SwoopingBat swoopingBat);
    public void TakeNextAction(SwoopingBat swoopingBat);
    public void Animate(Animator animator);
}
