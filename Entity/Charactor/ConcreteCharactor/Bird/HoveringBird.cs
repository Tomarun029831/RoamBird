using UnityEngine;

public class HoveringBird : Charactor
{
    [SerializeField] private float Amplitude;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rg;

    private Vector3 initPosition;
    private float loopTimer;

    void Awake()
    {
        initPosition = transform.position;
        Init();
    }

    void Update()
    {
        loopTimer = (loopTimer + Time.deltaTime) % 360;

        float yPos = Amplitude * Mathf.Sin(loopTimer * speed) + initPosition.y;
        rg.MovePosition(new(initPosition.x, yPos));
    }

    public override void Init() => transform.position = initPosition;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        float originY = Application.isPlaying ? initPosition.y : transform.position.y;
        Vector3 originPos = new(transform.position.x, originY, transform.position.z);

        // top
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(originPos + Vector3.up * Amplitude, 0.2f);

        // bottom
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(originPos - Vector3.up * Amplitude, 0.2f);
    }
#endif
}
