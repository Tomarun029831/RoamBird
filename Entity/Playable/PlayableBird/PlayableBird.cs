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
    private Vector2 initPosition;

    void Awake()
    {
        initPosition = transform.position;
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
        Invoke(nameof(CallSceneInitilaze), 2f);
    }
    private void SetState(PlayableBirdState state)
    {
        this.State = state;
        this.State.Animate(Animator);
    }

    private void CallSceneInitilaze() => SceneInitializer.InitializeScene();

    public override void Init()
    {
        transform.position = initPosition;
        animator.SetBool("isDie", false);
        SetStateToIdle();
    }

    void OnCollisionEnter2D(Collision2D collision2D) => State.OnCollisionEnter2D(collision2D, this);

    void OnTriggerEnter2D(Collider2D collider2D) => State.OnTriggerEnter2D(collider2D, this);
}
