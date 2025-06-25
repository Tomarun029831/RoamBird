using UnityEngine;

public class PlayableBirdDie : PlayableBirdState
{
    private PlayableBirdDie() { }

    private static class SingletonHolder
    {
        public static readonly PlayableBirdState instance = new PlayableBirdDie();
    }

    public static PlayableBirdState getInstance() => SingletonHolder.instance;

    public void Jump(PlayableBird playableBird) { return; }

    public void Animate(Animator animator)
    {
        animator.SetBool("isFly", false);
        animator.SetBool("isDie", true);
    }

    public void OnCollisionEnter2D(Collision2D collision2D, PlayableBird playableBird) { }
    public void OnTriggerEnter2D(Collider2D collider2D, PlayableBird playableBird) { }
}
