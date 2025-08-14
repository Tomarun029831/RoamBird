using UnityEngine;

public static class ScenePauser
{
    public static void PauseScene() => Time.timeScale = 0;
    public static void Resume() => Time.timeScale = 1;
}
