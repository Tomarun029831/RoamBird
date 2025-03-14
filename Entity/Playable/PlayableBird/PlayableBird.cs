using UnityEngine;

public class PlayableBird : Playable
{
    [SerializeField] private PlayableBirdScriptableObject playableBirdData;
    public PlayableBirdScriptableObject PlayableBirdData => playableBirdData;
    [SerializeField] private Rigidbody2D rg;
    public Rigidbody2D Rg => rg;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    [SerializeField] private Animator animator;
    public Animator Animator => animator;
    public PlayableBirdState State { private set; get; }

    void Awake()
    {
        SetState(PlayableBirdIdle.getInstance());
    }

    public override void Execute(Bind bind)
    {
        switch (bind)
        {
            case (Bind.Space):
                State.Jump(this);
                break;
            default:
                break;
        }
    }

    public override void FlipX() => spriteRenderer.flipX = !spriteRenderer.flipX;

    public void SetState(PlayableBirdState State)
    {
        this.State = State;
        State.Animate(animator);
    }

    void OnCollisionEnter2D(Collision2D collision2D) => State.OnCollisionEnter2D(collision2D, this);

    void OnTriggerEnter2D(Collider2D collider2D) => State.OnTriggerEnter2D(collider2D, this);
}
