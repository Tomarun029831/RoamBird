using UnityEngine;

public class TurnPad : MonoBehaviour, Gimmick
{
    [SerializeField] private TurnPadScriptableObjectScript turnPadData;

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Rigidbody2D rg = collision2D.rigidbody;
        float direction = System.Math.Sign(collision2D.transform.position.x - gameObject.transform.position.x);
        rg.linearVelocity = direction * turnPadData.KnockBackVelocity;
    }
}
