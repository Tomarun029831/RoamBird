using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Vector3 initPosition;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector2 startCoordinate;
    [SerializeField] private Vector2 endCoordinate;
    [SerializeField] private bool trackY;

    void Awake() => initPosition = transform.position;

    void LateUpdate()
    {
        if (trackY) TrackTargetY(target.transform.position.y);
        TrackTargetX(target.transform.position.x);
    }

    private void TrackTargetX(float targetX)
    {
        bool isOutOfRangeOnX = targetX < startCoordinate.x || endCoordinate.x < targetX;
        if (isOutOfRangeOnX) return;
        transform.position = new(targetX, transform.position.y, -10);
    }

    private void TrackTargetY(float targetY)
    {
        bool isOutOfRangeOnY = startCoordinate.y < targetY || targetY < endCoordinate.y;
        if (isOutOfRangeOnY) return;
        transform.position = new(transform.position.x, targetY, -10);
    }

    public void Init() => transform.position = initPosition;
}
