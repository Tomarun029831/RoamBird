using UnityEngine;

public class GlidingBat : Charactor
{
    [SerializeField] private Animator animator;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Rigidbody2D rg;

    public GlidingBatState state;

    private Vector3 initPosition;

    void Awake()
    {
        initPosition = transform.position;
        Init();
    }

    public override void Init()
    {
        transform.position = initPosition;
    }

}
