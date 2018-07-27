using UnityEngine;

namespace LUT.Event.Trigger
{
    public sealed class TriggerEventOnTriggerEnter2D : TriggerEventOn
    {
        public void OnTriggerEnter2D(Collider2D collider)
        {
            if (string.IsNullOrEmpty(_targetTag) || collider.CompareTag(_targetTag))
            {
                _eventToTrigger.Invoke();
                this.enabled = false;
            }
        }
    }
}
