using UnityEngine;

public class PlayableBird : Playable
{
    [SerializeField] private PlayableBirdScriptableObject PlayableBirdData;
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

    private void Jump()
    {
        // rg.linearVelocity = Vector2.zero;
        // rg.AddForce(new(200, 300));
        rg.linearVelocity = PlayableBirdData.JumpVelocity;
    }
}
