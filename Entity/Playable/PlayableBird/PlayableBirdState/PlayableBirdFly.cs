using UnityEngine;

public class PlayableBirdFly : PlayableBirdState
{
    private PlayableBirdFly() { }
    private static class SingletonHolder { public static readonly PlayableBirdState instance = new PlayableBirdFly(); }
    public static PlayableBirdState getInstance() => SingletonHolder.instance;

    public void Jump(PlayableBird playableBird)
    {
        Vector2 currentVelocity = new(Mathf.Abs(playableBird.Rg.linearVelocityX), playableBird.Rg.linearVelocityY);
        Vector2 maxVelocity = playableBird.PlayableBirdData.JumpVelocity;
        int nextDirection = playableBird.SpriteRenderer.flipX ? -1 : 1;
        bool IsNotMaxVelocityOnX = maxVelocity.x >= currentVelocity.x;
        if (IsNotMaxVelocityOnX) playableBird.Rg.linearVelocityX = nextDirection * maxVelocity.x;
        bool IsNotMaxVelocityOnY = maxVelocity.y >= currentVelocity.y;
        if (IsNotMaxVelocityOnY) playableBird.Rg.linearVelocityY = maxVelocity.y;
    }

    public void Animate(Animator animator) => animator.SetBool("isFly", true);

    public void OnCollisionEnter2D(Collision2D collision2D, PlayableBird playableBird)
    {
        if (collision2D.gameObject.CompareTag("Needle")) Dead(playableBird);
        if (collision2D.gameObject.CompareTag("Ground"))
        {
            Vector3 footOrigin = playableBird.transform.position + 0.3f * Vector3.down;
            RaycastHit2D downwardRaycast = Physics2D.Raycast(footOrigin, Vector2.down, 0.4f, LayerMask.GetMask("Ground"));
            bool isOnGround = downwardRaycast.collider == null;
            if (isOnGround) return;

            playableBird.SetStateToIdle();
        }
    }

    public void OnTriggerEnter2D(Collider2D collider2D, PlayableBird playableBird)
    {
        if (collider2D.CompareTag("Enemy")) Dead(playableBird);
        if (collider2D.CompareTag("Goal"))
        {
            StageProgressionTracker.StopTrack(true);
            SceneChanger.ChangeFarwardScene();
        }
    }

    private void Dead(PlayableBird playableBird)
    {
        playableBird.Rg.linearVelocity = Vector2.zero;
        playableBird.SetStateToDie();
    }
}
