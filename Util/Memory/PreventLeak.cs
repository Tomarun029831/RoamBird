using UnityEngine;

namespace Assets.Scripts.Util.Memory
{
    public static class PreventLeak
    {
        public static void DestroyPreventLeak(ref GameObject obj)
        {
            if (obj == null) { return; }

            GameObject.Destroy(obj);
            obj = null;
        }
    }
}

