using UnityEngine;

public class TurnPad : MonoBehaviour, Gimmick
{
    [SerializeField] private TurnPadScriptableObjectScript turnPadData;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        collision2D.gameObject.GetComponent<Playable>().FlipX(); // PERF: GetComponent is liner search => slow
        Rigidbody2D rg = collision2D.rigidbody;
        float nextDirection = System.Math.Sign(collision2D.transform.position.x - gameObject.transform.position.x);
        rg.linearVelocity = nextDirection * turnPadData.KnockBackVelocity;
    }
}
