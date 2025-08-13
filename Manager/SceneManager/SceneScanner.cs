using UnityEngine;

public static class SceneScanner
{
    public static Entity[] ScanAllEntities() => GameObject.FindObjectsByType<Entity>(FindObjectsSortMode.None);
    public static Playable[] ScanAllPlayables() => GameObject.FindObjectsByType<Playable>(FindObjectsSortMode.None);
    public static Charactor[] ScanAllCharactors() => GameObject.FindObjectsByType<Charactor>(FindObjectsSortMode.None);
    public static CameraHandler ScanCameraHandler() => GameObject.FindFirstObjectByType<CameraHandler>();
}
