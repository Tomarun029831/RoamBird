public static class SceneInitializer
{
    public static void InitializeScene()
    {
        Entity[] entities = SceneScanner.ScanAllEntities();
        CameraHandler cameraHandler = SceneScanner.ScanCameraHandler();
        foreach (Entity entity in entities)
        {
            entity.Init();
        }
        cameraHandler.Init();
    }
}
