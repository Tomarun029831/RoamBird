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
    public PlayableBirdState state { private set; get; }
    private Vector2 initPosition, initVelocity;
    private bool initFlipX;

    void Awake()
    {
        initPosition = transform.position;
        initVelocity = rg.linearVelocity;
        initFlipX = spriteRenderer.flipX;
        SetState(PlayableBirdIdle.getInstance());
    }

    void OnCollisionEnter2D(Collision2D collision2D) => state.OnCollisionEnter2D(collision2D, this);

    void OnTriggerEnter2D(Collider2D collider2D) => state.OnTriggerEnter2D(collider2D, this);

    public override void Execute(Bind bind)
    {
        switch (bind)
        {
            case (Bind.Space):
                state.Jump(this);
                break;
            default:
                break;
        }
    }

    public override void FlipX() => spriteRenderer.flipX = !spriteRenderer.flipX;

    public void SetStateToIdle()
    {
        SetState(PlayableBirdIdle.getInstance());
    }

    public void SetStateToFly()
    {
        SetState(PlayableBirdFly.getInstance());
    }

    public void SetStateToDie()
    {
        SetState(PlayableBirdDie.getInstance());
        Rg.linearVelocity = Vector2.zero;
        Invoke(nameof(CallSceneInitilaze), 2f);
        StageProgressionTracker.StopTrack(false);
    }

    private void SetState(PlayableBirdState state)
    {
        this.state = state;
        this.state.Animate(Animator);
    }

    private void CallSceneInitilaze() => SceneInitializer.InitializeScene();

    public override void Init()
    {
        transform.position = initPosition;
        rg.linearVelocity = initVelocity;
        spriteRenderer.flipX = initFlipX;
        animator.SetBool("isDie", false);
        SetStateToIdle();
    }
}
