using UnityEngine;

public class PlayableBird : MonoBehaviour, Playable
{
    [SerializeField]
    private PlayableBirdScriptableObject PlayableBirdData;
    private Rigidbody2D rg;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private State state;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
}
