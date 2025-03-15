using UnityEngine;

public class PlayableBirdFly : PlayableBirdState
{
    private PlayableBirdFly() { }

    private static class SingletonHolder
    {
        public static readonly PlayableBirdState instance = new PlayableBirdFly();
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
    }


    public void Animate(Animator animator)
    {
        animator.SetBool("isFly", true);
    }

    public void OnCollisionEnter2D(Collision2D collision2D, PlayableBird playableBird)
    {
        if (collision2D.gameObject.CompareTag("Needle"))
        {
            Dead(playableBird);
        }

        if (collision2D.gameObject.CompareTag("Ground"))
        {
            Vector3 origin = playableBird.transform.position + 0.3f * Vector3.down;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, Vector2.down, 0.4f, LayerMask.GetMask("Ground"));
            // Debug.DrawRay(origin, 0.4f * Vector2.down, Color.red, 1);
            // Debug.Log(raycastHit2D.collider.name);
            if (raycastHit2D.collider != null)
            {
                playableBird.SetStateToIdle();
            }
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
        playableBird.SetStateToDie();
    }
}
