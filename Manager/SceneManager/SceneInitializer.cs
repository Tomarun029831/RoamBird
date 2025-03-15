using UnityEngine;

public static class SceneInitializer
{
    public static void InitializeScene()
    {
        Entity[] entities = SceneScanner.ScanAllEntities();
        foreach (Entity entity in entities)
        {
            entity.Init();
        }
    }
}
