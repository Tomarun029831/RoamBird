using UnityEngine;

public class PlayableBirdIdle : PlayableBirdState
{
    private PlayableBirdIdle() { }

    private static class SingletonHolder
    {
        public static readonly PlayableBirdState instance = new PlayableBirdIdle();
    }

    public static PlayableBirdState getInstance() => SingletonHolder.instance;

    public void Jump(PlayableBird playableBird)
    {
        Vector2 velocity = new(Mathf.Abs(playableBird.Rg.linearVelocityX), playableBird.Rg.linearVelocityY);
        Vector2 maxVelocity = playableBird.PlayableBirdData.JumpVelocity;
        int direction = playableBird.SpriteRenderer.flipX ? -1 : 1;

        if (maxVelocity.x >= velocity.x)
        {
            playableBird.Rg.linearVelocityX = direction * maxVelocity.x;
        }
        if (maxVelocity.y >= velocity.y)
        {
            playableBird.Rg.linearVelocityY = maxVelocity.y;
        }
        playableBird.SetState(PlayableBirdFly.getInstance());
    }

    public void Animate(Animator animator)
    {
        animator.SetBool("isFly", false);
    }

    public void OnCollisionEnter2D(Collision2D collision2D, PlayableBird playableBird)
    {
        if (collision2D.gameObject.CompareTag("Needle"))
        {
            Dead(playableBird);
        }

        if (collision2D.gameObject.CompareTag("Goal"))
        {
        }
    }

    public void OnTriggerEnter2D(Collider2D collider2D, PlayableBird playableBird)
    {
        if (collider2D.CompareTag("Enemy"))
        {
            Dead(playableBird);
        }
    }

    private void Dead(PlayableBird playableBird)
    {
        playableBird.Rg.linearVelocity = Vector2.zero;
        playableBird.SetState(PlayableBirdDie.getInstance());
    }
}
