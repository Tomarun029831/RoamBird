using UnityEngine;

public interface SwoopingBatState
{
    public void Update(SwoopingBat swoopingBat);

    public void TakeNextAction(SwoopingBat swoopingBat);

    public void Animate(Animator animator);
}
