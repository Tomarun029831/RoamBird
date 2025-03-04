using UnityEngine;

public class PlayableBird : Playable
{
    [SerializeField] private PlayableBirdScriptableObject playableBirdData;
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    private State state;

    public override void Execute(Bind bind)
    {
        switch (bind)
        {
            case (Bind.Space):
                Jump();
                break;
            default:
                break;
        }
    }

    public override void FlipX()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    private void Jump()
    {
        Vector2 velocity = new(Mathf.Abs(rg.linearVelocityX), rg.linearVelocityY);
        Vector2 maxVelocity = playableBirdData.JumpVelocity;
        int direction = spriteRenderer.flipX ? -1 : 1;
        if (maxVelocity.x >= velocity.x)
        {
            rg.linearVelocityX = direction * playableBirdData.JumpVelocity.x;
        }
        if (maxVelocity.y >= velocity.y)
        {
            rg.linearVelocityY = playableBirdData.JumpVelocity.y;
        }
    }
}
