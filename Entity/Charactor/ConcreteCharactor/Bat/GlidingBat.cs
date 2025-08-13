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
    private bool initFlipY;
    private bool initFlipX;
    private bool currentFlipY;

    void Awake()
    {
        initFlipY = spriteRenderer.flipY;
        initPosition = transform.position;
        initFlipX = spriteRenderer.flipX;
        Init();
    }

    public void FixedUpdate() => state.FixedUpdate(this);

    public void Hide()
    {
        spriteRenderer.enabled = false;
        batCollider2D.enabled = false;
    }

    public void Turn()
    {
        currentFlipY = !currentFlipY;
        spriteRenderer.flipX = !currentFlipY;
    }

    public void FlipToRight() => spriteRenderer.flipX = false;

    public void FlipToLeft() => spriteRenderer.flipX = true;

    public bool GetRestDirection() => currentFlipY;

    public void MoveX(float vectorX)
    {
        if (vectorX <= 0) { return; }
        rg.MovePosition(new Vector2(transform.position.x + (currentFlipY ? vectorX : -vectorX), transform.position.y));
    }

    public void MoveY(float vectorY) => rg.MovePosition(new Vector2(transform.position.x, transform.position.y + vectorY));

    public void Move(Vector2 vector2)
    {
        if (vector2.x <= 0) { return; }
        rg.MovePosition(new Vector2(transform.position.x + (currentFlipY ? vector2.x : -vector2.x), transform.position.y + vector2.y));
    }

    public override void Init()
    {
        internalTimer = 0;
        currentFlipY = initFlipY;
        spriteRenderer.flipY = initFlipY;
        spriteRenderer.flipX = initFlipX;
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

    public void SetStateToFlyAway() => SetState(GlidingBatFlyAway.getInstance());

    private void SetState(GlidingBatState state)
    {
        this.state = state;
        this.state.Animate(animator);
    }
}
