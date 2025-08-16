using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Util.Delay
{
    /// <summary>
    /// TYPE: KeyCode for PC, TYPE: int for Mobile
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputHolder<T>
    {
        private readonly Dictionary<T, bool> keyInputs;
        public IReadOnlyDictionary<T, bool> KeyInputs => keyInputs;
        public InputHolder() => keyInputs = new Dictionary<T, bool>();
        public InputHolder(int capacity) => keyInputs = new Dictionary<T, bool>(capacity);
        public InputHolder(T predefinedKey) => keyInputs = new Dictionary<T, bool> { { predefinedKey, false } };

        public InputHolder(IEnumerable<T> predefinedKeys) => keyInputs = predefinedKeys.ToDictionary(k => k, _ => false);

        public void HoldInput(T key) => keyInputs[key] = true;

        public bool ConsumeInput(T key)
        {
            if (keyInputs.TryGetValue(key, out bool isHeld) && isHeld)
            {
                keyInputs[key] = false;
                return true;
            }
            return false;
        }
    }
}
