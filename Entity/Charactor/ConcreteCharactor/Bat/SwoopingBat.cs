using UnityEngine;

public class SwoopingBat : Charactor
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rg;
    [SerializeField] private Collider2D batCollider2D;
    [System.NonSerialized] public float internalTimer;
    public SwoopingBatState state { get; private set; }
    private Vector3 initPosition;

    void Awake()
    {
        initPosition = transform.position;
        Init();
    }

    void FixedUpdate() => state.FixedUpdate(this);

    public void FlipToRight() => spriteRenderer.flipX = true;

    public void FlipToLeft() => spriteRenderer.flipX = false;

    public bool GetRestDirection() => spriteRenderer.flipX;

    public void Turn() => spriteRenderer.flipX = !spriteRenderer.flipX;

    public void MoveX(float vectorX)
    {
        if (vectorX <= 0) { return; }
        rg.MovePosition(new Vector2(transform.position.x + (spriteRenderer.flipX ? vectorX : -vectorX), transform.position.y));
    }

    public void MoveY(float vectorY) => rg.MovePosition(new Vector2(transform.position.x, transform.position.y + vectorY));

    public void Hide()
    {
        spriteRenderer.enabled = false;
        batCollider2D.enabled = false;
    }

    public override void Init()
    {
        spriteRenderer.enabled = true;
        batCollider2D.enabled = true;
        transform.position = initPosition;
        internalTimer = 0;
        SetStateToRest();
    }

    public void SetStateToRest()
    {
        rg.gravityScale = 0;
        SetState(SwoopingBatRest.getInstance());
    }

    public void SetStateToSwoop()
    {
        rg.gravityScale = 1;
        SetState(BatSwoop.getInstance());
    }

    public void SetStateToHover()
    {
        rg.gravityScale = 0;
        rg.linearVelocity = Vector2.zero;
        SetState(SwoopingBatHover.getInstance());
    }

    public void SetStateToFlyAway()
    {
        rg.gravityScale = 0;
        SetState(SwoopingBatFlyAway.getInstance());
    }

    private void SetState(SwoopingBatState state)
    {
        this.state = state;
        this.state.Animate(animator);
    }
}
