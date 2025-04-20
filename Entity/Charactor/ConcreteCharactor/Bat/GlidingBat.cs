using UnityEngine;

public class GlidingBat : Charactor
{
    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rg;

    [SerializeField] private Collider2D batCollider2D;

    [System.NonSerialized] public float internalTimer;

    public GlidingBatState state { get; private set; }

    private Vector3 initPosition;
    private bool initSpriteRendererFlipY;

    void Awake()
    {
        initSpriteRendererFlipY = spriteRenderer.flipY;
        initPosition = transform.position;
        Init();
    }

    public void FixedUpdate() => state.FixedUpdate(this);

    public void Hide()
    {
        spriteRenderer.enabled = false;
        batCollider2D.enabled = false;
    }

    public void FlipToRight() => spriteRenderer.flipX = false;

    public void FlipToLeft() => spriteRenderer.flipX = true;

    public bool GetRestDirection() => spriteRenderer.flipY;

    public void MoveX(float vectorX)
    {
        rg.MovePosition(new Vector2(transform.position.x + vectorX, transform.position.y));
    }

    public void MoveY(float vectorY)
    {
        rg.MovePosition(new Vector2(transform.position.x, transform.position.y + vectorY));
    }

    public override void Init()
    {
        internalTimer = 0;
        spriteRenderer.flipY = initSpriteRendererFlipY;
        spriteRenderer.enabled = true;
        batCollider2D.enabled = true;
        transform.position = initPosition;
        SetStateToRest();
    }

    public void SetStateToRest()
    {
        rg.gravityScale = 0;
        SetState(GlidingBatRest.getInstance());
    }

    public void SetStateToGlide()
    {
        if (spriteRenderer.flipY)
            FlipToLeft();
        else
            FlipToRight();

        spriteRenderer.flipY = false;
        SetState(BatGlide.getInstance());
    }

    public void SetStateToFlyAway()
    {
        SetState(GlidingBatFlyAway.getInstance());
    }

    private void SetState(GlidingBatState state)
    {
        this.state = state;
        this.state.Animate(animator);
    }
}
