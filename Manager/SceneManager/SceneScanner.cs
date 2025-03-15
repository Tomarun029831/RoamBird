using UnityEngine;

public static class SceneScanner
{
    public static Entity[] ScanAllEntities()
    {
        return GameObject.FindObjectsByType<Entity>(FindObjectsSortMode.None);
    }

    public static Playable[] ScanAllPlayables()
    {
        return GameObject.FindObjectsByType<Playable>(FindObjectsSortMode.None);
    }

    public static Charactor[] ScanAllCharactors()
    {
        return GameObject.FindObjectsByType<Charactor>(FindObjectsSortMode.None);
    }
}
