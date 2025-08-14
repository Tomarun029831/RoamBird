using UnityEngine;

public interface GlidingBatState
{
    public void FixedUpdate(GlidingBat glidingBat);
    public void TakeNextAction(GlidingBat glidingBat);
    public void Animate(Animator animator);
}
