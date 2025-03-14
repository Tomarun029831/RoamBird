using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void LateUpdate()
    {
        TrackTargetX(target.transform.position.x);
    }

    private void TrackTargetX(float targetX)
    {
        transform.position = new(targetX, transform.position.y, -10);
    }

    private void TrackTargetY(float targetY)
    {
        transform.position = new(transform.position.x, targetY, -10);
    }
}
