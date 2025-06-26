using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject runnerObj = new GameObject("CoroutineRunner");
                Object.DontDestroyOnLoad(runnerObj);
                _instance = runnerObj.AddComponent<CoroutineRunner>();
            }
            return _instance;
        }
    }
}
