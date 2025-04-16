using UnityEngine;

public interface GlidingBatState
{
    public void TakeNextAction(GlidingBat glidingBat);

    public void Animate(Animator animator);
}
