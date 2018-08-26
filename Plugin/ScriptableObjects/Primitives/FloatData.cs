using UnityEngine;

namespace LUT.Primitive
{
    [CreateAssetMenu(fileName = "FloatData", menuName = "Data/Primitive/Float", order = -10)]
    public sealed class FloatData : ScriptableObject
    {
        [SerializeField]
        private float _value;

#pragma warning disable 0414
        // this variable is used by FloatDataInspector through deserialization
        [SerializeField, HideInInspector]
        private bool _lock;
#pragma warning restore 0414

        public float Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }

        [ContextMenu("Lock")]
        private void Lock()
        {
            _lock = true;
        }
        [ContextMenu("Unlock")]
        private void Unlock()
        {
            _lock = false;
        }
    }
}
