using UnityEngine;

[CreateAssetMenu(fileName = "PlayableBirdScriptableObject", menuName = "Scriptable Objects/PlayableBirdScriptableObject")]
public class PlayableBirdScriptableObject : ScriptableObject
{
    [SerializeField] private Vector2 jumpVelocity;
    public Vector2 JumpVelocity => JumpVelocity;
}
