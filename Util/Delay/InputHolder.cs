using System.Collections.Generic;

namespace Assets.Scripts.Util.Delay
{
    /// <summary>
    /// TYPE: KeyCode for PC, TYPE: int for Mobile
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class InputHolder<T>
    {
        private Dictionary<T, bool> key_inputs;
        public Dictionary<T, bool> Key_Inputs => key_inputs;

        public InputHolder() { key_inputs = new(); }
        public InputHolder(int _capacity) { key_inputs = new Dictionary<T, bool>(_capacity); }
        public InputHolder(T _predefinedKey)
        {
            key_inputs = new() { { _predefinedKey, false } };
        }
        public InputHolder(IEnumerable<T> _predefinedKeys)
        {
            key_inputs = new();
            foreach (var ele in _predefinedKeys)
            {
                key_inputs[ele] = false;
            }
        }

        public void HoldInput(T _key)
        {
            key_inputs[_key] = true;
        }

        public bool ConsumeInput(T _key)
        {

            if (!key_inputs.ContainsKey(_key)) // process on key_inputs(Field) doesn't contain _key
            {
                return false;
            }
            else // process on key_inputs(Field) contain _key
            {
                if (key_inputs[_key])
                {
                    key_inputs[_key] = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            // return key_inputs[_key] && (key_inputs[_key] = false) == false;
        }
    }
}
