using UnityEngine;

public class SwoopingBat : Charactor
{
    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rg;

    [System.NonSerialized] public float internalTimer;

    public SwoopingBatState State { get; private set; }

    private Vector3 initPosition;

    void Awake()
    {
        initPosition = transform.position;
        Init();
    }

    void Update()
    {
        State.Update(this);
    }

    public void FlipToRight()
    {
        spriteRenderer.flipX = true;
    }

    public void FlipToLeft()
    {
        spriteRenderer.flipX = false;
    }

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
        this.State = state;
        this.State.Animate(animator);
    }
}
