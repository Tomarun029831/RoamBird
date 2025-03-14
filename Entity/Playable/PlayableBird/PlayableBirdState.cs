using UnityEngine;

public interface PlayableBirdState
{
    public void Jump(PlayableBird playableBird);

    public void Animate(Animator animator);

    public void OnCollisionEnter2D(Collision2D collision2D, PlayableBird playableBird);

    public void OnTriggerEnter2D(Collider2D collider2D, PlayableBird playableBird);
}
