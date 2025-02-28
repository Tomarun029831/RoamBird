using UnityEngine;

[CreateAssetMenu(fileName = "TurnPadScriptableObjectScript", menuName = "Scriptable Objects/TurnPadScriptableObjectScript")]
public class TurnPadScriptableObjectScript : ScriptableObject
{
    [SerializeField] private Vector2 knockBackVelocity;
    public Vector2 KnockBackVelocity => knockBackVelocity;
}
