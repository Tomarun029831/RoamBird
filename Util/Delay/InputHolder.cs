using System.Collections.Generic;

namespace Assets.Scripts.Util.Delay
{
    /// <summary>
    /// TYPE: KeyCode for PC, TYPE: int for Mobile
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputHolder<T>
    {
        private Dictionary<T, bool> keyInputs;
        public Dictionary<T, bool> KeyInputs => keyInputs;

        public InputHolder() => keyInputs = new();
        public InputHolder(int _capacity) => keyInputs = new Dictionary<T, bool>(_capacity);
        public InputHolder(T _predefinedKey) => keyInputs = new() { { _predefinedKey, false } };
        public InputHolder(IEnumerable<T> predefinedKeys)
        {
            keyInputs = new();
            foreach (var ele in predefinedKeys)
            {
                keyInputs[ele] = false;
            }
        }

        public void HoldInput(T key) => keyInputs[key] = true;

        public bool ConsumeInput(T key)
        {
            if (!keyInputs.ContainsKey(key)) // process on keyInputs(Field) doesn't contain key
            {
                return false;
            }
            else // process on keyInputs(Field) contain key
            {
                if (keyInputs[key])
                {
                    keyInputs[key] = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // return keyInputs[key] && (keyInputs[key] = false) == false;
        }
    }
}
