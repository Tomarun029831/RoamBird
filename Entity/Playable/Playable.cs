using UnityEngine;

public abstract class Playable : Entity
{
    public enum Bind
    {
        Space
    }

    public abstract void FlipX();

    public abstract void Execute(Bind bind);
}
